<template>
  <role-permission-applications-list :edit-mode="editMode" :role-id="roleId" />
</template>

<script lang="ts">
import { computed, defineComponent } from "vue";

import { useRoute } from "vue-router";

import RolePermissionApplicationsList from "../components/RolePermissionApplicationsList.vue";

export default defineComponent({
  components: {
    RolePermissionApplicationsList,
  },
  setup(props) {
    const route = useRoute();

    const editMode = computed(() => route.hash.includes("editMode=true"));

    return {
      editMode,
      roleId: route.params.roleId as string,
    };
  },
});
</script>
