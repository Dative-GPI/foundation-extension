<template>
  <v-app style="background-color: transparent">
    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>

<script lang="ts">
import { defineComponent } from "vue";

import { useExtensionHost } from "@dative-gpi/foundation-extension-shared-ui";

export default defineComponent({
  setup(_props) {
    useExtensionHost();

    return {};
  },
});
</script>
