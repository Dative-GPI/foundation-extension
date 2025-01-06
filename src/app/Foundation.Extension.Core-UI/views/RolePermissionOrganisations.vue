<template>
  <FEPermissionsGrid
    v-if="roleOrganisationPermissions"
    :permissions="roleOrganisationPermissions.permissionIds"
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
import { useRolePermissionOrganisations } from "../composables";

import FEPermissionsGrid from "../components/FEPermissionsGrid.vue";

export default defineComponent({
  name: "RoleOrganisationPermissionOrganisations",
  components: {
    FEPermissionsGrid
  },
  setup() {
    const { fetch: getRoleOrganisationPermissions, entity: roleOrganisationPermissions } = useRolePermissionOrganisations();
    const { openDialog } = useExtensionCommunicationBridge();
    const router = useRouter();
    
    const roleId = ref<string | null>(null);
    const editable = ref(false);

    watch(router.currentRoute, () => {
      roleId.value = router.currentRoute.value.params["roleId"] as string | null;
      if (roleId.value) {
        getRoleOrganisationPermissions(roleId.value);
      }
      editable.value = router.currentRoute.value.query["editable"] === "true";
    }, { immediate: true });

    return {
      roleOrganisationPermissions,
      editable,
      roleId,
      UPDATE_ROLE_PERMISSION_ORGANISATION_DIALOG_PATH,
      openDialog,
      uuidv4
    };
  }
});
</script>