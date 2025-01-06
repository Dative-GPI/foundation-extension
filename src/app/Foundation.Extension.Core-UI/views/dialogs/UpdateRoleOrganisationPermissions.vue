<template>
  <FEDialogSubmit
    :load="updatingRoleOrganisationPermissions"
    @submit="onSubmit"
    v-model="dialog"
  >
    <template
      #body
    >
      <FEEditPermissionsGrid
        v-if="roleOrganisationPermissions"
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
    const { fetch : updateRoleOrganisationPermissions, fetching: updatingRoleOrganisationPermissions } = useUpdateRolePermissionOrganisations();
    const { fetch: getRoleOrganisationPermissions, entity: roleOrganisationPermissions } = useRolePermissionOrganisations();
    const router = useRouter();

    const dialog = ref(true);

    const permissionsIds = ref<string[]>([]);
    const roleId = ref<string | null>(null);

    const onSubmit = async () => {
      if (roleId.value) {
        try {
          await updateRoleOrganisationPermissions(roleId.value, {
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
        getRoleOrganisationPermissions(roleId.value);
      }
    }, { immediate: true });

    watch(roleOrganisationPermissions, (prev, next) => {
      if (prev != next) {
        permissionsIds.value = roleOrganisationPermissions.value.permissionIds.slice();
      }
    });

    return {
      updatingRoleOrganisationPermissions,
      roleOrganisationPermissions,
      permissionsIds,
      dialog,
      onSubmit
    }
  }
});
</script>