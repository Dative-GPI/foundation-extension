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
    FEDialogSubmit,
  },
  setup() {
    const { fetch : updateServiceAccountRoleOrganisation, fetching: updatingServiceAccountRoleOrganisation } = useUpdateServiceAccountRoleOrganisation();
    const { fetch: getServiceAccountRoleOrganisation, entity: serviceAccountRoleOrganisation } = useServiceAccountRoleOrganisation();
    const router = useRouter();

    const dialog = ref(true);

    const permissionsIds = ref<string[]>([]);
    const roleId = ref<string | null>(null);

    const onSubmit = async () => {
      if (roleId.value) {
        try {
          await updateServiceAccountRoleOrganisation(roleId.value, {
            permissionIds: permissionsIds.value
          });
        }
        finally {
          dialog.value = false;
        }
      }
    }

    watch(router.currentRoute, () => {
      roleId.value = router.currentRoute.value.params["roleId"] as string | null;
      if (roleId.value) {
        getServiceAccountRoleOrganisation(roleId.value);
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