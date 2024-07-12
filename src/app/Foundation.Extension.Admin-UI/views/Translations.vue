<template>
  <translation-list :edit-mode="editMode" />
</template>

<script lang="ts">
import TranslationList from "../components/TranslationList.vue";

import { defineComponent, computed } from "vue";
import { useRoute } from "vue-router";

export default defineComponent({
  name: "Translations",
  components: {
    TranslationList,
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
