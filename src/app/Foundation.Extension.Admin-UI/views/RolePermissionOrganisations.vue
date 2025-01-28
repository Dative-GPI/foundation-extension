<template>
  <RoleOrganisationTypePermissionsList
    :roleOrganisationTypeId="roleOrganisationTypeId"
    :organisationTypeId="organisationTypeId"  
    :editMode="editMode"
  />
</template>

<script lang="ts">
import { computed, defineComponent } from "vue";
import { useRoute } from "vue-router";

import RoleOrganisationTypePermissionsList from "../components/organisationTypes/RoleOrganisationTypePermissionsList.vue";

export default defineComponent({
  name: "RoleOrganisationTypePermissions",
  components: {
    RoleOrganisationTypePermissionsList,
  },
  setup() {
    const route = useRoute();

    const editMode = computed(() => route.hash.includes("editMode=true"));

    return {
      roleOrganisationTypeId: route.params.roleId as string,
      organisationTypeId: route.params.organisationTypeId as string,
      editMode
    };
  }
});
</script>