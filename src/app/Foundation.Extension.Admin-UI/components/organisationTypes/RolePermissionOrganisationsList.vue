<template>
  <FSCol
    v-if="!fetching"
    :gap="24"
  >
    <FSRow
      align="left-center"
    >
      <FSTextField
        label="Search"
        v-model="search"
        clearable
      ></FSTextField>
      <FSButton
        label="Select all"
        color="primary"
        v-if="editMode"
        @click="updateAll(true)"
      > </FSButton>
      <FSButton
        label="Disable all"
        color="primary"
        v-if="editMode"
        @click="updateAll(false)"
      > </FSButton>
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
        > {{ category.label }} </FSRow>
        <FSRow>
          <FSButton
            label="Select all"
            color="primary"
            v-if="editMode"
            @click="updateCategory(true, category.id)"
          >
          </FSButton>
          <FSButton
            label="Disable all"
            color="primary"
            v-if="editMode"
            @click="updateCategory(false, category.id)"
          >
          </FSButton>
        </FSRow>
        <FSRow
          v-for="permission in category.options"
          :key="permission.id"
        >
          <FSSpan
            font="text-body align-self-center"
          > {{ permission.label }} </FSSpan>
          <v-spacer></v-spacer>
          <FSSwitch
            v-if="editMode"
            ref="element"
            :modelValue="permissionIds.includes(permission.id)"
            @update:modelValue="updatePermissionIds(permission.id)"
            color="success"
          />
          <template
            v-else
          >
            <FSIcon
              v-if="permissionIds.includes(permission.id)"
              color="success"
            > mdi-checkbox-marked-circle</FSIcon>
            <FSIcon
              v-else
              color="error"
            > mdi-close-circle</FSIcon>
          </template>
          <v-divider></v-divider>
        </FSRow>
      </FSCol>
    </FSRow>
  </FSCol>
</template>
<script lang="ts">
import { defineComponent, ref, onMounted, computed, watch } from "vue";
import { useRoute } from "vue-router";
import _ from "lodash";

import {
  usePermissionOrganisationCategories,
  usePermissionOrganisationTypes,
  useRolePermissionOrganisations,
  useUpdateRolePermissionOrganisations,
} from "../../composables";

export default defineComponent({
  name: "RolePermissionOrganisationsList",
  props: {
    editMode: {
      type: Boolean,
      default: true,
    },
    roleId: {
      type: String,
      required: true
    },
    organisationTypeId: {
      type: String,
      required: true
    },
  },
  setup(props) {
    const { fetch: getPermissionOrganisationCategories, entity: categories } = usePermissionOrganisationCategories();
    const { getMany: getPermissionOrganisationTypes, entities: permissionOrganisationTypes } =
      usePermissionOrganisationTypes();
    const { get: getRolePermissionOrganisations, entity: rolePermissionOrganisations } =
      useRolePermissionOrganisations();
    const { fetch: update } = useUpdateRolePermissionOrganisations();
    const route = useRoute();

    const element = ref<HTMLElement | null>(null);

    const search = ref("");
    const fetching = ref(true);

    const permissionIds = ref<string[]>([]);

    const init = async () => {
      await getPermissionOrganisationCategories();
      await getPermissionOrganisationTypes({ organisationTypeId: props.organisationTypeId  });

      fetchRoleOrganisation();
    };

    const fetchRoleOrganisation = async () => {
      fetching.value = true;
      await getRolePermissionOrganisations(props.roleId);
      permissionIds.value = rolePermissionOrganisations.value!.permissionIds.map((p) => p);
      fetching.value = false;
    };

    const filteredPermissionOrganisationTypes = computed(() => {
      if (search.value == null || search.value === "") {return permissionOrganisationTypes.value;}
      return permissionOrganisationTypes.value.filter((p) => {
        return (
          p.permissionCode.toLowerCase().includes(search.value.toLowerCase()) ||
          p.permissionLabel.toLowerCase().includes(search.value.toLowerCase())
        );
      });
    });

    const categoriesAndPermissions = computed(() => {
      return categories.value.map((cat, index) => ({
        id: index.toString(),
        label: cat.label,
        options: filteredPermissionOrganisationTypes.value
          .filter((p) => p.permissionCode.startsWith(cat.prefix))
          .map((p) => ({
            id: p.permissionId,
            label: p.permissionLabel,
          })),
      }));
    });

    const updateAll = (enableAll: boolean) => {
      if (enableAll) {
        permissionIds.value = filteredPermissionOrganisationTypes.value.map((p) => p.permissionId);
      } else {
        permissionIds.value = [];
      }
    };

    const updateCategory = (enabledAll: boolean, categoryId: string) => {
      let category = categoriesAndPermissions.value.find((c) => c.id === categoryId);
      if (!category) {return;}
      let permissions = category?.options.map((p) => p.id);
      if (enabledAll) {
        permissionIds.value = Array.from(new Set([...permissionIds.value, ...permissions]));
      } else {
        permissionIds.value = permissionIds.value.filter((p) => !permissions.includes(p));
      }
    };

    const updatePermissionIds = (permissionId: string) => {
      if (permissionIds.value.includes(permissionId)) {
        permissionIds.value = permissionIds.value.filter((p) => p !== permissionId);
      } else {
        permissionIds.value = [...permissionIds.value, permissionId];
      }
    };

    function save() {
      update(props.roleId, { permissionIds: permissionIds.value });
    }

    const synchronizePermissions = async () => {
      if (!props.editMode) {return;}
      await save();
    };

    const debouncedsynchronizePermissions = _.debounce(synchronizePermissions, 500);

    watch(permissionIds, debouncedsynchronizePermissions);

    watch(
      () => props.editMode,
      () => {
        if (props.editMode == false) {
          save();
        }
      }
    );

    onMounted(init);

    return {
      categoriesAndPermissions,
      search,
      fetching,
      permissionIds,
      element,
      filteredPermissionOrganisationTypes,
      updatePermissionIds,
      updateAll,
      updateCategory,
    };
  },
});
</script>
<style scoped>
tbody tr {
  cursor: pointer;
}

tbody tr:hover {
  background-color: #e6efff;
}
</style>
