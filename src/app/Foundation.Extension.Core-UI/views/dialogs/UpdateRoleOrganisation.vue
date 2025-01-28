<template>
  <FEDialogSubmit
    :load="updatingRoleOrganisation"
    @submit="onSubmit"
    v-model="dialog"
  >
    <template
      #body
    >
      <FEEditPermissionsGrid
        v-if="roleOrganisation"
        v-model="permissionsIds"
      />
    </template>
  </FEDialogSubmit>
</template>

<script lang="ts">
import { defineComponent, ref, watch } from "vue";
import { useRouter } from "vue-router";

import { useRoleOrganisation, useUpdateRoleOrganisation } from "../../composables";

import FEDialogSubmit from "@dative-gpi/foundation-extension-shared-ui/components/FEDialogSubmit.vue";

import FEEditPermissionsGrid from "../../components/FEEditPermissionsGrid.vue";

export default defineComponent({
  name: "UpdateRoleOrganisation",
  components: {
    FEEditPermissionsGrid,
    FEDialogSubmit
  },
  setup() {
    const { update : updateRoleOrganisation, updating: updatingRoleOrganisation } = useUpdateRoleOrganisation();
    const { get: getRoleOrganisation, entity: roleOrganisation } = useRoleOrganisation();
    const router = useRouter();

    const dialog = ref(true);

    const permissionsIds = ref<string[]>([]);
    const roleOrganisationId = ref<string | null>(null);

    const onSubmit = async () => {
      if (roleOrganisationId.value) {
        try {
          await updateRoleOrganisation(roleOrganisationId.value, {
            permissionIds: permissionsIds.value
          });
        }
        finally {
          dialog.value = false;
        }
      }
    }

    watch(router.currentRoute, () => {
      roleOrganisationId.value = router.currentRoute.value.params["roleOrganisationId"] as string | null;
      if (roleOrganisationId.value) {
        getRoleOrganisation(roleOrganisationId.value);
      }
    }, { immediate: true });

    watch(roleOrganisation, (prev, next) => {
      if (prev != next) {
        permissionsIds.value = roleOrganisation.value.permissionIds.slice();
      }
    });

    return {
      updatingRoleOrganisation,
      roleOrganisation,
      permissionsIds,
      dialog,
      onSubmit
    }
  }
});
</script>