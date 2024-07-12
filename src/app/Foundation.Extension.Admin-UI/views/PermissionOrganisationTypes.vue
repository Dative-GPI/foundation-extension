<template>
  <permission-organisation-types-list :edit-mode="editMode" :organisation-type-id="organisationTypeId" />
</template>

<script lang="ts">
import { computed, defineComponent } from "vue";

import { useRoute } from "vue-router";

import PermissionOrganisationTypesList from "../components/organisationTypes/PermissionOrganisationTypesList.vue";

export default defineComponent({
  components: {
    PermissionOrganisationTypesList,
  },
  setup(props) {
    const route = useRoute();

    const editMode = computed(() => route.hash.includes("editMode=true"));

    return {
      editMode,
      organisationTypeId: route.params.organisationTypeId as string,
    };
  },
});
</script>
