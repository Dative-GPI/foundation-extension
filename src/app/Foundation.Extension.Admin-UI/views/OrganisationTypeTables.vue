<template>
    <organisation-type-tables-list :edit-mode="editMode" />
</template>
  
<script lang="ts">
import OrganisationTypeTablesList from "../components/organisationTypes/OrganisationTypeTablesList.vue";

import { defineComponent, computed } from "vue";
import { useRoute } from "vue-router";

export default defineComponent({
    name: "OrganisationTypeTables",
    components: {
        OrganisationTypeTablesList,
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
  