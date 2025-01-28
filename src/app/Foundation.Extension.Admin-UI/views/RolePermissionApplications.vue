<template>
  <RoleApplicationPermissionsList
    :roleApplicationId="roleApplicationId"
    :editMode="editMode"
  />
</template>

<script lang="ts">
import { computed, defineComponent } from "vue";
import { useRoute } from "vue-router";

import RoleApplicationPermissionsList from "../components/RoleApplicationPermissionsList.vue";

export default defineComponent({
  components: {
    RoleApplicationPermissionsList,
  },
  setup() {
    const route = useRoute();

    const editMode = computed(() => route.hash.includes("editMode=true"));

    return {
      roleApplicationId: route.params.roleId as string,
      editMode
    };
  }
});
</script>