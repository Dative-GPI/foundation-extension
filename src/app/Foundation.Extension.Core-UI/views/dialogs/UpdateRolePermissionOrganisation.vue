<template>
  <FEDialogSubmit
    :load="updatingRoleOrganisationPermission"
    @submit="onSubmit"
    v-model="dialog"
  >
    <template
      #body
    >
      <FEEditPermissionsGrid
        v-if="roleOrganisationPermission"
        v-model="permissionsIds"
      />
    </template>
  </FEDialogSubmit>
</template>

<script lang="ts">
import { defineComponent, ref, watch } from "vue";
import { useRouter } from "vue-router";

import { useRolePermissionOrganisation, useUpdateRolePermissionOrganisation } from "../../composables";

import FEDialogSubmit from "@dative-gpi/foundation-extension-shared-ui/components/FEDialogSubmit.vue";

import FEEditPermissionsGrid from "../../components/FEEditPermissionsGrid.vue";

export default defineComponent({
  name: "UpdateRoleOrganisation",
  components: {
    FEEditPermissionsGrid,
    FEDialogSubmit,
  },
  setup() {
    const { fetch : updateRoleOrganisationPermission, fetching: updatingRoleOrganisationPermission } = useUpdateRolePermissionOrganisation();
    const { fetch: getRoleOrganisationPermission, entity: roleOrganisationPermission } = useRolePermissionOrganisation();
    const router = useRouter();

    const dialog = ref(true);

    const permissionsIds = ref<string[]>([]);
    const roleId = ref<string | null>(null);

    const onSubmit = async () => {
      if (roleId.value) {
        try {
          await updateRoleOrganisationPermission(roleId.value, {
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
        getRoleOrganisationPermission(roleId.value);
      }
    }, { immediate: true });

    watch(roleOrganisationPermission, (prev, next) => {
      if (prev != next) {
        permissionsIds.value = roleOrganisationPermission.value.permissionIds.slice();
      }
    });

    return {
      updatingRoleOrganisationPermission,
      roleOrganisationPermission,
      permissionsIds,
      dialog,
      onSubmit
    }
  }
});
</script>