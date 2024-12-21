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
    FEDialogSubmit,
  },
  setup() {
    const { fetch : updateRoleOrganisation, fetching: updatingRoleOrganisation } = useUpdateRoleOrganisation();
    const { fetch: getRoleOrganisation, entity: roleOrganisation } = useRoleOrganisation();
    const router = useRouter();

    const dialog = ref(true);

    const permissionsIds = ref<string[]>([]);
    const roleId = ref<string | null>(null);

    const onSubmit = async () => {
      if (roleId.value) {
        try {
          await updateRoleOrganisation(roleId.value, {
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
        getRoleOrganisation(roleId.value);
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