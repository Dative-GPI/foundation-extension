<template>
  <FEPermissionsGrid
    v-if="userOrganisation"
    :permissions="userOrganisation.permissionIds"
  />
</template>

<script lang="ts">
import { defineComponent, ref, watch } from "vue";
import { useRouter } from "vue-router";

import { useUserOrganisation } from "../composables";

import FEPermissionsGrid from "../components/FEPermissionsGrid.vue";

export default defineComponent({
  name: "UserOrganisationPermissions",
  components: {
    FEPermissionsGrid
  },
  setup() {
    const { get: getUserOrganisation, entity: userOrganisation } = useUserOrganisation();
    const router = useRouter();
    
    const userOrganisationId = ref<string | null>(null);

    watch(router.currentRoute, () => {
      userOrganisationId.value = router.currentRoute.value.params["userOrganisationId"] as string | null;
      if (userOrganisationId.value) {
        getUserOrganisation(userOrganisationId.value);
      }
    }, { immediate: true });

    return {
      userOrganisation
    };
  }
});
</script>