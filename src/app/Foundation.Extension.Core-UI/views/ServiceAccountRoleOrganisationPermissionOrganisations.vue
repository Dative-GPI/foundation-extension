<template>
  <FEPermissionsGrid
    v-if="serviceAccountRoleOrganisation"
    :permissions="serviceAccountRoleOrganisation.permissionIds"
  >
    <template
      #toolbar
    >
      <FSButton
        v-if="editable"
        prependIcon="mdi-shield-lock-outline"
        :label="$tr('ui.role.edit-permissions', 'Edit permissions')"
        @click="openDialog(UPDATE_SERVICE_ACCOUNT_ROLE_ORGANISATION_DIALOG_PATH(roleId, uuidv4()))"
      />
    </template>
  </FEPermissionsGrid>
</template>

<script lang="ts">
import { defineComponent, ref, watch } from "vue";
import { useRouter } from "vue-router";

import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui/composables";
import { uuidv4 } from "@dative-gpi/bones-ui";

import { UPDATE_SERVICE_ACCOUNT_ROLE_ORGANISATION_DIALOG_PATH } from "../config";
import { useServiceAccountRoleOrganisation } from "../composables";

import FEPermissionsGrid from "../components/FEPermissionsGrid.vue";

export default defineComponent({
  name: "RoleOrganisationPermissionOrganisations",
  components: {
    FEPermissionsGrid
  },
  setup() {
    const { fetch: getServiceAccountRoleOrganisation, entity: serviceAccountRoleOrganisation } = useServiceAccountRoleOrganisation();
    const { openDialog } = useExtensionCommunicationBridge();
    const router = useRouter();
    
    const roleId = ref<string | null>(null);
    const editable = ref(false);

    watch(router.currentRoute, () => {
      roleId.value = router.currentRoute.value.params["roleId"] as string | null;
      if (roleId.value) {
        getServiceAccountRoleOrganisation(roleId.value);
      }
      editable.value = router.currentRoute.value.query["editable"] === "true";
    }, { immediate: true });

    return {
      serviceAccountRoleOrganisation,
      editable,
      roleId,
      UPDATE_SERVICE_ACCOUNT_ROLE_ORGANISATION_DIALOG_PATH,
      openDialog,
      uuidv4
    };
  }
});
</script>