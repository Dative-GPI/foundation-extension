<template>
  <FSCol
    v-if="!fetching"
    :gap="24"
  >
    <FSRow
      align="bottom-left"
    >
      <FSCol
        style="max-width: 300px !important"
      >
        <FSSearchField
          :hideHeader="true"
          v-model="search"
        />
      </FSCol>
      <FSButton
        v-if="editMode"
        label="Enable all"
        color="primary"
        @click="updateAll(true)"
      />
      <FSButton
        v-if="editMode"
        label="Disable all"
        color="primary"
        @click="updateAll(false)"
      />
    </FSRow>
    <FSRow
      v-for="category in categoriesAndPermissions"
      :key="category.id"
    >
      <FSCol
        style="max-width: 30%"
      >
        <FSRow
          font="text-title"
        >
          {{ category.label }}
        </FSRow>
        <FSRow>
          <FSButton
            v-if="editMode"
            label="Enable all"
            color="primary"
            @click="updateCategory(true, category.id)"
          />
          <FSButton
            v-if="editMode"
            label="Disable all"
            color="primary"
            @click="updateCategory(false, category.id)"
          />
        </FSRow>
        <FSRow
          v-for="permission in category.options"
          :key="permission.id"
        >
          <FSSpan
            font="text-body align-self-center"
          >
            {{ permission.label }}
          </FSSpan>
          <v-spacer />
          <FSSwitch
            v-if="editMode"
            color="success"
            :modelValue="permissionIds.includes(permission.id)"
            @update:modelValue="updatePermissionIds(permission.id)"
          />
          <template
            v-else
          >
            <FSIcon
              v-if="permissionIds.includes(permission.id)"
              color="success"
            >
              mdi-checkbox-marked-circle
            </FSIcon>
            <FSIcon
              v-else
              color="error"
            >
              mdi-close-circle
            </FSIcon>
          </template>
          <v-divider />
        </FSRow>
      </FSCol>
    </FSRow>
  </FSCol>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, computed, watch } from "vue";
import _ from "lodash";

import { usePermissionOrganisations, usePermissionOrganisationTypes, usePermissionOrganisationCategories, useUpsertPermissionOrganisationTypes } from "../../composables";

export default defineComponent({
  name: "PermissionOrganisationTypeList",
  props: {
    organisationTypeId: {
      type: String,
      required: true,
      default: ""
    },
    editMode: {
      type: Boolean,
      required: false,
      default: true
    }
  },
  setup(props) {
    const { getMany: getPermissionOrganisationCategories, entities: permissionOrganisationCategories } = usePermissionOrganisationCategories();
    const { getMany: getPermissionOrganisationTypes, entities: permissionOrganisationTypes, fetching } = usePermissionOrganisationTypes();
    const { getMany: getPermissionOrganisations, entities: permissionOrganisations } = usePermissionOrganisations();
    const { fetch: upsert } = useUpsertPermissionOrganisationTypes();

    const search = ref("");

    const permissionIds = ref<string[]>([]);

    const init = async () => {
      await getPermissionOrganisationCategories();
      await getPermissionOrganisations();

      await getPermissionOrganisationTypes({ organisationTypeId: props.organisationTypeId });
    };

    const filteredPermissionOrganisation = computed(() => {
      if (search.value == null || search.value === "") {
        return permissionOrganisations.value;
      }
      return permissionOrganisations.value.filter((p) => (
        p.code.toLowerCase().includes(search.value.toLowerCase()) ||
        p.label.toLowerCase().includes(search.value.toLowerCase())
      ));
    });

    const categoriesAndPermissions = computed(() => {
      return permissionOrganisationCategories.value.map((cat, index) => ({
        id: index.toString(),
        label: cat.label,
        options: filteredPermissionOrganisation.value
          .filter((p) => p.code.startsWith(cat.prefix))
          .map((p) => ({
            id: p.id,
            label: p.label
          }))
      }));
    });

    const updateAll = (enableAll: boolean) => {
      if (enableAll) {
        permissionIds.value = filteredPermissionOrganisation.value.map((p) => p.id);
      }
      else {
        permissionIds.value = [];
      }
    };

    const updateCategory = (enabledAll: boolean, categoryId: string) => {
      let category = categoriesAndPermissions.value.find((c) => c.id === categoryId);
      if (!category) {
        return;
      }
      let permissions = category?.options.map((p) => p.id);
      if (enabledAll) {
        permissionIds.value = Array.from(new Set([...permissionIds.value, ...permissions]));
      }
      else {
        permissionIds.value = permissionIds.value.filter((p) => !permissions.includes(p));
      }
    };

    const updatePermissionIds = (permissionId: string) => {
      if (permissionIds.value.includes(permissionId)) {
        permissionIds.value = permissionIds.value.filter((p) => p !== permissionId);
      }
      else {
        permissionIds.value = [...permissionIds.value, permissionId];
      }
    };

    async function save() {
      await upsert([{ organisationTypeId: props.organisationTypeId, permissionIds: permissionIds.value }]);
    };

    const synchronizePermissions = async () => {
      if (!props.editMode) {
        return;
      }
      await save();
    };
    
    const debouncedsynchronizePermissions = _.debounce(synchronizePermissions, 500);

    watch(permissionIds, debouncedsynchronizePermissions);

    watch(() => props.editMode, () => {
      if (props.editMode == false) {
        save();
      }
    });

    watch(() => permissionOrganisationTypes.value, () => {
      permissionIds.value = permissionOrganisationTypes.value.map((p) => p.permissionId);
    });

    onMounted(init);

    return {
      categoriesAndPermissions,
      search,
      fetching,
      permissionIds,
      filteredPermissionOrganisation,
      updatePermissionIds,
      updateCategory,
      updateAll
    };
  }
});
</script>