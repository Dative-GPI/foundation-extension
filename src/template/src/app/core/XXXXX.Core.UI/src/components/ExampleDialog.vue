<template>
  <FEDialog
    :value="false"
    :height="200"
  >
    test dialog
  </FEDialog>
</template>

<script lang="ts">
import { defineComponent } from "vue";

import FEDialog from "@dative-gpi/foundation-extension-shared-ui/components/FEDialog.vue";

export default defineComponent({
  name: "ExampleDialog",
  components: {
    FEDialog
  },
  setup() {
    return {};
  },
});
</script>

<style scoped></style>
