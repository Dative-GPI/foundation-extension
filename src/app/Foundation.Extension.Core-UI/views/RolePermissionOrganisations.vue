<template>
  <role-permission-organisations-list
    :edit-mode="editMode"
    :role-id="roleId"
  />
</template>

<script lang="ts">
import { computed, defineComponent } from "vue";

import { useRoute } from "vue-router";

import RolePermissionOrganisationsList from "../components/RolePermissionOrganisationsList.vue";

export default defineComponent({
  components: {
    RolePermissionOrganisationsList,
  },
  setup() {
    const route = useRoute();

    const editMode = computed(() => route.hash.includes("editMode=true"));

    return {
      editMode,
      roleId: route.params.roleId as string,
    };
  },
});
</script>
