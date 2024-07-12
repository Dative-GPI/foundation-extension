<template>
  <v-container fluid class="pa-0 ma-0">
    <entities-list />
  </v-container>
</template>

<script lang="ts">
import EntitiesList from "../components/entities/EntitiesList.vue";

import { defineComponent } from "vue";

export default defineComponent({
  name: "Entities",
  components: {
    EntitiesList,
  },
  setup(props) {
    return {};
  },
});
</script>

<style scoped></style>
