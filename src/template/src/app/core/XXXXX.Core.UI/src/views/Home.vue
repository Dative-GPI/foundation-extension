<template>
  <ExampleComponent />
</template>

<script lang="ts">
import { defineComponent } from "vue";
import ExampleComponent from "@/components/ExampleComponent.vue";

export default defineComponent({
  name: "Home",
  components: {
    ExampleComponent
  },
  setup() {
    return {

    };
  },
});
</script>

<style scoped></style>
