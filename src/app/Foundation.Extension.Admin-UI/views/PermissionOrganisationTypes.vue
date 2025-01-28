<template>
  <PermissionOrganisationTypesList
    :organisationTypeId="organisationTypeId"
    :editMode="editMode"
  />
</template>

<script lang="ts">
import { computed, defineComponent } from "vue";
import { useRoute } from "vue-router";

import PermissionOrganisationTypesList from "../components/organisationTypes/PermissionOrganisationTypesList.vue";

export default defineComponent({
  components: {
    PermissionOrganisationTypesList,
  },
  setup() {
    const route = useRoute();

    const editMode = computed(() => route.hash.includes("editMode=true"));

    return {
      organisationTypeId: route.params.organisationTypeId as string,
      editMode
    };
  }
});
</script>