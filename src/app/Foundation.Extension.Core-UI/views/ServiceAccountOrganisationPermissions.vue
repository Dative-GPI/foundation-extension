<template>
  <FEPermissionsGrid
    v-if="serviceAccountOrganisation"
    :permissions="serviceAccountOrganisation.permissionIds"
  />
</template>

<script lang="ts">
import { defineComponent, ref, watch } from "vue";
import { useRouter } from "vue-router";

import { useServiceAccountOrganisation } from "../composables";

import FEPermissionsGrid from "../components/FEPermissionsGrid.vue";

export default defineComponent({
  name: "UserOrganisationPermissions",
  components: {
    FEPermissionsGrid
  },
  setup() {
    const { get: getServiceAccountOrganisation, entity: serviceAccountOrganisation } = useServiceAccountOrganisation();
    const router = useRouter();
    
    const serviceAccountOrganisationId = ref<string | null>(null);

    watch(router.currentRoute, () => {
      serviceAccountOrganisationId.value = router.currentRoute.value.params["serviceAccountOrganisationId"] as string | null;
      if (serviceAccountOrganisationId.value) {
        getServiceAccountOrganisation(serviceAccountOrganisationId.value);
      }
    }, { immediate: true });

    return {
      serviceAccountOrganisation
    };
  }
});
</script>