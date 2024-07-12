<template>
  <tables-list :edit-mode="editMode" />
</template>

<script lang="ts">
import TablesList from "../components/TablesList.vue";

import { defineComponent, computed } from "vue";
import { useRoute } from "vue-router";

export default defineComponent({
  name: "Tables",
  components: {
    TablesList,
  },
  setup(props) {
    const route = useRoute();

    const editMode = computed(() => {
      return route.hash.includes("editMode=true");
    });

    return {
      editMode,
    };
  },
});
</script>

<style scoped></style>
