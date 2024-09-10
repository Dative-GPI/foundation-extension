<template>
  <FEDialogRemove />
</template>

<script lang="ts">
import { defineComponent } from "vue";

import FEDialogRemove from "@dative-gpi/foundation-extension-shared-ui/components/FEDialogRemove.vue";

export default defineComponent({
  name: "Remove",
  components: {
    FEDialogRemove,
  }
});
</script>