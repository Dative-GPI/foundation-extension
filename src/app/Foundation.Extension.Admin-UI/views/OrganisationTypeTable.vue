<template>
  <FSButton label="Edit"
    color="primary"
    prepend-icon="mdi-pencil"
    v-if="!editMode"
    @click="editMode = true">
  </FSButton>
  <FSButton label="Save"
    color="primary"
    prepend-icon="mdi-content-save-outline"
    v-if="editMode"
    @click="editMode = false">
  </FSButton>
  <organisation-type-table-details :edit-mode="editMode"
    :organisation-type-id="organisationTypeId"
    :table-id="tableId" />
</template>

<script lang="ts">
import OrganisationTypeTableDetails from "../components/organisationTypes/OrganisationTypeTableDetails.vue";

import { defineComponent, computed, ref } from "vue";
import { useRoute } from "vue-router";

export default defineComponent({
  name: "Table",
  components: {
    OrganisationTypeTableDetails,
  },
  setup(props) {
    const route = useRoute();
    const editMode = ref<boolean>(false);

    return {
      editMode,
      organisationTypeId: route.params.organisationTypeId as string,
      tableId: route.params.tableId as string,
    };
  },
});
</script>

<style scoped></style>
