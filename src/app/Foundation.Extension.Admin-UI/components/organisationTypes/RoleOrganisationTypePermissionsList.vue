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

import { usePermissionOrganisationCategories, usePermissionOrganisationTypes, useRoleOrganisationType, useUpdateRoleOrganisationType, } from "../../composables";

export default defineComponent({
  name: "RoleOrganisationTypePermissionsList",
  props: {
    organisationTypeId: {
      type: String,
      required: true
    },
    roleOrganisationTypeId: {
      type: String,
      required: true
    },
    editMode: {
      type: Boolean,
      default: true,
    }
  },
  setup(props) {
    const { getMany: getPermissionOrganisationCategories, entities: permissionOrganisationCategories } = usePermissionOrganisationCategories();
    const { getMany: getPermissionOrganisationTypes, entities: permissionOrganisationTypes } = usePermissionOrganisationTypes();
    const { get: getRoleOrganisationType, entity: roleOrganisationType, getting: fetching } = useRoleOrganisationType();
    const { update: updateRoleOrganisationType } = useUpdateRoleOrganisationType();

    const search = ref("");

    const permissionIds = ref<string[]>([]);

    const init = async () => {
      await getPermissionOrganisationCategories();
      await getPermissionOrganisationTypes({ organisationTypeId: props.organisationTypeId  });

      await getRoleOrganisationType(props.roleOrganisationTypeId);
    };

    const filteredPermissionOrganisationTypes = computed(() => {
      if (search.value == null || search.value === "") {
        return permissionOrganisationTypes.value;
      }
      return permissionOrganisationTypes.value.filter((p) => (
        p.permissionCode.toLowerCase().includes(search.value.toLowerCase()) ||
        p.permissionLabel.toLowerCase().includes(search.value.toLowerCase())
      ));
    });

    const categoriesAndPermissions = computed(() => {
      return permissionOrganisationCategories.value.map((cat, index) => ({
        id: index.toString(),
        label: cat.label,
        options: filteredPermissionOrganisationTypes.value
          .filter((p) => p.permissionCode.startsWith(cat.prefix))
          .map((p) => ({
            id: p.permissionId,
            label: p.permissionLabel
          }))
      }));
    });

    const updateAll = (enableAll: boolean) => {
      if (enableAll) {
        permissionIds.value = filteredPermissionOrganisationTypes.value.map((p) => p.permissionId);
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
      await updateRoleOrganisationType(props.roleOrganisationTypeId, { permissionIds: permissionIds.value });
    }

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

    watch(() => roleOrganisationType.value, () => {
      permissionIds.value = roleOrganisationType.value.permissionIds.map((p) => p);
    });

    onMounted(init);

    return {
      categoriesAndPermissions,
      search,
      fetching,
      permissionIds,
      filteredPermissionOrganisationTypes,
      updatePermissionIds,
      updateCategory,
      updateAll
    };
  }
});
</script>