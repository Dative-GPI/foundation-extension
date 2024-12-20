<template>
  <FEPermissionsGrid
    :admin="true"
  />
</template>

<script lang="ts">
import { defineComponent } from "vue";

import FEPermissionsGrid from "../components/FEPermissionsGrid.vue";

export default defineComponent({
  name: "AdminPermissionOrganisations",
  components: {
    FEPermissionsGrid
  }
});
</script>