<template>
  <FEPermissionsGrid
    v-if="roleOrganisationType"
    :permissions="roleOrganisationType.permissionIds"
  />
</template>

<script lang="ts">
import { defineComponent, ref, watch } from "vue";
import { useRouter } from "vue-router";

import { uuidv4 } from "@dative-gpi/bones-ui";

import { useRoleOrganisationType } from "../composables";

import FEPermissionsGrid from "../components/FEPermissionsGrid.vue";

export default defineComponent({
  name: "RoleOrganisationTypePermissions",
  components: {
    FEPermissionsGrid
  },
  setup() {
    const { get: getRoleOrganisationType, entity: roleOrganisationType } = useRoleOrganisationType();
    const router = useRouter();
    
    const roleOrganisationTypeId = ref<string | null>(null);

    watch(router.currentRoute, () => {
      roleOrganisationTypeId.value = router.currentRoute.value.params["roleOrganisationTypeId"] as string | null;
      if (roleOrganisationTypeId.value) {
        getRoleOrganisationType(roleOrganisationTypeId.value);
      }
    }, { immediate: true });

    return {
      roleOrganisationType,
      uuidv4
    };
  }
});
</script>