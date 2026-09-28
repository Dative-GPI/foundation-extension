<template>
  <FEDialogSubmit
    :load="updatingServiceAccountRoleOrganisation"
    @submit="onSubmit"
    v-model="dialog"
  >
    <template
      #body
    >
      <FEEditPermissionsGrid
        v-if="serviceAccountRoleOrganisation"
        v-model="permissionsIds"
      />
    </template>
  </FEDialogSubmit>
</template>

<script lang="ts">
import { defineComponent, ref, watch } from "vue";
import { useRouter } from "vue-router";

import { useServiceAccountRoleOrganisation, useUpdateServiceAccountRoleOrganisation } from "../../composables";

import FEDialogSubmit from "@dative-gpi/foundation-extension-shared-ui/components/FEDialogSubmit.vue";

import FEEditPermissionsGrid from "../../components/FEEditPermissionsGrid.vue";

export default defineComponent({
  name: "UpdateServiceAccountRoleOrganisation",
  components: {
    FEEditPermissionsGrid,
    FEDialogSubmit
  },
  setup() {
    const { update : updateServiceAccountRoleOrganisation, updating: updatingServiceAccountRoleOrganisation } = useUpdateServiceAccountRoleOrganisation();
    const { get: getServiceAccountRoleOrganisation, entity: serviceAccountRoleOrganisation } = useServiceAccountRoleOrganisation();
    const router = useRouter();

    const dialog = ref(true);

    const permissionsIds = ref<string[]>([]);
    const serviceAccountRoleOrganisationId = ref<string | null>(null);

    const onSubmit = async () => {
      if (serviceAccountRoleOrganisationId.value) {
        try {
          await updateServiceAccountRoleOrganisation(serviceAccountRoleOrganisationId.value, {
            permissionIds: permissionsIds.value
          });
        }
        finally {
          dialog.value = false;
        }
      }
    }

    watch(router.currentRoute, () => {
      serviceAccountRoleOrganisationId.value = router.currentRoute.value.params["serviceAccountRoleOrganisation"] as string | null;
      if (serviceAccountRoleOrganisationId.value) {
        getServiceAccountRoleOrganisation(serviceAccountRoleOrganisationId.value);
      }
    }, { immediate: true });

    watch(serviceAccountRoleOrganisation, (prev, next) => {
      if (prev != next) {
        permissionsIds.value = serviceAccountRoleOrganisation.value.permissionIds.slice();
      }
    });

    return {
      updatingServiceAccountRoleOrganisation,
      serviceAccountRoleOrganisation,
      permissionsIds,
      dialog,
      onSubmit
    }
  }
});
</script>