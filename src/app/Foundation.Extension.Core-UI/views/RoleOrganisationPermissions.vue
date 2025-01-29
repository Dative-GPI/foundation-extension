<template>
  <FEPermissionsGrid
    v-if="roleOrganisation"
    :permissions="roleOrganisation.permissionIds"
  >
    <template
      #toolbar
    >
      <FSButton
        v-if="$pm.every(FOUNDATION_PERMISSIONS.ROLEORGANISATION_UPDATE)"
        prependIcon="mdi-shield-lock-outline"
        :label="$tr('ui.role-organisation.edit-permissions', 'Edit permissions')"
        @click="openDialog(UPDATE_ROLE_ORGANISATION_DIALOG_PATH(roleOrganisationId, uuidv4()))"
      />
    </template>
  </FEPermissionsGrid>
</template>

<script lang="ts">
import { defineComponent, ref, watch } from "vue";
import { useRouter } from "vue-router";

import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui/composables";
import { uuidv4 } from "@dative-gpi/bones-ui";

import { FOUNDATION_PERMISSIONS, UPDATE_ROLE_ORGANISATION_DIALOG_PATH } from "../config";
import { useRoleOrganisation } from "../composables";

import FEPermissionsGrid from "../components/FEPermissionsGrid.vue";

export default defineComponent({
  name: "RoleOrganisationPermissions",
  components: {
    FEPermissionsGrid
  },
  setup() {
    const { get: getRoleOrganisation, entity: roleOrganisation } = useRoleOrganisation();
    const { openDialog } = useExtensionCommunicationBridge();
    const router = useRouter();
    
    const roleOrganisationId = ref<string | null>(null);

    watch(router.currentRoute, () => {
      roleOrganisationId.value = router.currentRoute.value.params["roleOrganisationId"] as string | null;
      if (roleOrganisationId.value) {
        getRoleOrganisation(roleOrganisationId.value);
      }
    }, { immediate: true });

    return {
      FOUNDATION_PERMISSIONS,
      roleOrganisationId,
      roleOrganisation,
      UPDATE_ROLE_ORGANISATION_DIALOG_PATH,
      openDialog,
      uuidv4
    };
  }
});
</script>