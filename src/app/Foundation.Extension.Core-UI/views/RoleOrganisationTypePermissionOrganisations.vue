<template>
  <FEPermissionsGrid
    v-if="roleOrganisationType"
    :permissions="roleOrganisationType.permissionIds"
  />
</template>

<script lang="ts">
import { defineComponent, ref, watch } from "vue";
import { useRouter } from "vue-router";

import { useRoleOrganisationType } from "../composables";

import FEPermissionsGrid from "../components/FEPermissionsGrid.vue";

export default defineComponent({
  name: "RoleOrganisationTypePermissionOrganisations",
  components: {
    FEPermissionsGrid
  },
  setup() {
    const { fetch: getRoleOrganisationType, entity: roleOrganisationType } = useRoleOrganisationType();
    const router = useRouter();
    
    const roleId = ref<string | null>(null);

    watch(router.currentRoute, () => {
      roleId.value = router.currentRoute.value.params["roleId"] as string | null;
      if (roleId.value) {
        getRoleOrganisationType(roleId.value);
      }
    }, { immediate: true });

    return {
      roleOrganisationType
    };
  }
});
</script>