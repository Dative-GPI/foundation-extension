<template>
  <FEPermissionsGrid
    v-if="userOrganisationPermission"
    :permissions="userOrganisationPermission.permissionIds"
  />
</template>

<script lang="ts">
import { defineComponent, ref, watch } from "vue";
import { useRouter } from "vue-router";

import { useUserPermissionOrganisation } from "../composables";
import { UserType } from "../domain/enums";

import FEPermissionsGrid from "../components/FEPermissionsGrid.vue";

export default defineComponent({
  name: "UserOrganisationPermissionOrganisations",
  components: {
    FEPermissionsGrid
  },
  setup() {
    const { fetch: getUserOrganisationPermission, entity: userOrganisationPermission } = useUserPermissionOrganisation();
    const router = useRouter();
    
    const userId = ref<string | null>(null);
    const userType = ref<UserType>(UserType.None);

    watch(router.currentRoute, () => {
      userId.value = router.currentRoute.value.params["userId"] as string | null;
      userType.value = parseInt(router.currentRoute.value.params["userType"] as string) ?? UserType.None;
      if (userId.value) {
        getUserOrganisationPermission(userId.value, userType.value);
      }
    }, { immediate: true });

    return {
      userOrganisationPermission
    };
  }
});
</script>