<template>
  <FEPermissionsGrid
    v-if="roleOrganisationPermission"
    :permissions="roleOrganisationPermission.permissionIds"
  >
    <template
      #toolbar
    >
      <FSButton
        v-if="editable"
        prependIcon="mdi-shield-lock-outline"
        :label="$tr('ui.role.edit-permissions', 'Edit permissions')"
        @click="openDialog(UPDATE_ROLE_PERMISSION_ORGANISATION_DIALOG_PATH(roleId, uuidv4()))"
      />
    </template>
  </FEPermissionsGrid>
</template>

<script lang="ts">
import { defineComponent, ref, watch } from "vue";
import { useRouter } from "vue-router";

import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui/composables";
import { uuidv4 } from "@dative-gpi/bones-ui";

import { UPDATE_ROLE_PERMISSION_ORGANISATION_DIALOG_PATH } from "../config";
import { useRolePermissionOrganisation } from "../composables";

import FEPermissionsGrid from "../components/FEPermissionsGrid.vue";

export default defineComponent({
  name: "RoleOrganisationPermissionOrganisations",
  components: {
    FEPermissionsGrid
  },
  setup() {
    const { fetch: getRoleOrganisationPermission, entity: roleOrganisationPermission } = useRolePermissionOrganisation();
    const { openDialog } = useExtensionCommunicationBridge();
    const router = useRouter();
    
    const roleId = ref<string | null>(null);
    const editable = ref(false);

    watch(router.currentRoute, () => {
      roleId.value = router.currentRoute.value.params["roleId"] as string | null;
      if (roleId.value) {
        getRoleOrganisationPermission(roleId.value);
      }
      editable.value = router.currentRoute.value.query["editable"] === "true";
    }, { immediate: true });

    return {
      roleOrganisationPermission,
      editable,
      roleId,
      UPDATE_ROLE_PERMISSION_ORGANISATION_DIALOG_PATH,
      openDialog,
      uuidv4
    };
  }
});
</script>