<template>
  <role-permission-organisations-list
    :organisationTypeId="organisationTypeId"  
    :edit-mode="editMode"
    :role-id="roleId"
  />
</template>

<script lang="ts">
import { computed, defineComponent } from "vue";

import { useRoute } from "vue-router";

import RolePermissionOrganisationsList from "../components/organisationTypes/RolePermissionOrganisationsList.vue";

export default defineComponent({
  components: {
    RolePermissionOrganisationsList,
  },
  setup(props) {
    const route = useRoute();

    const editMode = computed(() => route.hash.includes("editMode=true"));

    return {
      editMode,
      roleId: route.params.roleId as string,
      organisationTypeId: route.params.organisationTypeId as string,
    };
  },
});
</script>
