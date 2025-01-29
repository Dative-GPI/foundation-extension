<template>
  <FEPermissionsGrid
    v-if="currentUserOrganisation"
    :permissions="currentUserOrganisation.permissionIds"
  />
</template>

<script lang="ts">
import { defineComponent } from "vue";

import { useCoreExtension } from "../composables";

import FEPermissionsGrid from "../components/FEPermissionsGrid.vue";

export default defineComponent({
  name: "MePermissions",
  components: {
    FEPermissionsGrid
  },
  setup() {
    const { currentUserOrganisation } = useCoreExtension();

    return {
      currentUserOrganisation
    };
  }
});
</script>