<template>
  <ExampleDialog/>
</template>

<script lang="ts">
import { defineComponent } from "vue";

import ExampleDialog from "../components/ExampleDialog.vue";

export default defineComponent({
  name: "DialogView",
  components: {
    ExampleDialog
  },
  setup() {
    return {};
  },
});
</script>

<style scoped></style>
