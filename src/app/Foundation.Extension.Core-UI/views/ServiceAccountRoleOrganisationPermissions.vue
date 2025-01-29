<template>
  <FEPermissionsGrid
    v-if="serviceAccountRoleOrganisation"
    :permissions="serviceAccountRoleOrganisation.permissionIds"
  >
    <template
      #toolbar
    >
      <FSButton
        v-if="$pm.every(FOUNDATION_PERMISSIONS.SERVICEACCOUNTORGANISATION_MANAGE)"
        prependIcon="mdi-shield-lock-outline"
        :label="$tr('ui.service-account-role-organisation.edit-permissions', 'Edit permissions')"
        @click="openDialog(UPDATE_SERVICE_ACCOUNT_ROLE_ORGANISATION_DIALOG_PATH(serviceAccountRoleOrganisationId, uuidv4()))"
      />
    </template>
  </FEPermissionsGrid>
</template>

<script lang="ts">
import { defineComponent, ref, watch } from "vue";
import { useRouter } from "vue-router";

import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui/composables";
import { uuidv4 } from "@dative-gpi/bones-ui";

import { FOUNDATION_PERMISSIONS, UPDATE_SERVICE_ACCOUNT_ROLE_ORGANISATION_DIALOG_PATH } from "../config";
import { useServiceAccountRoleOrganisation } from "../composables";

import FEPermissionsGrid from "../components/FEPermissionsGrid.vue";

export default defineComponent({
  name: "ServiceAccountRoleOrganisationPermissions",
  components: {
    FEPermissionsGrid
  },
  setup() {
    const { get: getServiceAccountRoleOrganisation, entity: serviceAccountRoleOrganisation } = useServiceAccountRoleOrganisation();
    const { openDialog } = useExtensionCommunicationBridge();
    const router = useRouter();
    
    const serviceAccountRoleOrganisationId = ref<string | null>(null);

    watch(router.currentRoute, () => {
      serviceAccountRoleOrganisationId.value = router.currentRoute.value.params["serviceAccountRoleOrganisationId"] as string | null;
      if (serviceAccountRoleOrganisationId.value) {
        getServiceAccountRoleOrganisation(serviceAccountRoleOrganisationId.value);
      }
    }, { immediate: true });

    return {
      serviceAccountRoleOrganisationId,
      serviceAccountRoleOrganisation,
      FOUNDATION_PERMISSIONS,
      UPDATE_SERVICE_ACCOUNT_ROLE_ORGANISATION_DIALOG_PATH,
      openDialog,
      uuidv4
    };
  }
});
</script>