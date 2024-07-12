<template>
  <v-app
    style="background-color: transparent"
  >
    <v-main>
      <router-view
        v-if="done"
      />
    </v-main>
  </v-app>
</template>

<script lang="ts">
import { defineComponent } from "vue";

import { useCoreExtension } from "@dative-gpi/foundation-extension-core-ui";

export default defineComponent({
  setup() {
    const { done } = useCoreExtension();

    return {
      done
    }
  }
});

</script>
