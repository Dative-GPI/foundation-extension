<template>
  <FSButton
    label="Edit"
    color="primary"
    prepend-icon="mdi-pencil"
    v-if="!editMode"
    @click="editMode = true"
  >
  </FSButton>
  <FSButton
    label="Save"
    color="primary"
    prepend-icon="mdi-content-save-outline"
    v-if="editMode"
    @click="editMode = false"
  >
  </FSButton>
  {{ tableId }}
  <TableDetails
    v-if="tableId"
    :edit-mode="editMode"
    :table-id="tableId"
  />
</template>

<script lang="ts">
import TableDetails from "../components/TableDetails.vue";

import { defineComponent, ref } from "vue";
import { useRoute } from "vue-router";

export default defineComponent({
  name: "Table",
  components: {
    TableDetails,
  },
  setup() {
    const route = useRoute();
    const editMode = ref<boolean>(false);

    return {
      editMode,
      tableId: route.params.tableId as string,
    };
  },
});
</script>

<style scoped></style>
