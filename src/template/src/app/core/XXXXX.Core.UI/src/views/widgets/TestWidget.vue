<template>
  <FEWidget
    @update:meta="meta = $event"
    @update:width="width = $event"
    @update:height="height = $event"
  >
    <FSCol
      height="100%"
      :padding="12"
    >
      <FSText
        v-if="meta"
        font="text-h2"
      >
        {{ meta.label }}
      </FSText>
      <FSText
        v-if="width && height"
        font="text-body"
      >
        {{ `I am a ${width} x ${height} widget.` }}
      </FSText>
    </FSCol>
  </FEWidget>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";

import FEWidget from '@dative-gpi/foundation-extension-shared-ui/components/FEWidget.vue';

export default defineComponent({
  name: "TestWidget",
  components: {
    FEWidget
  },
  setup() {
    const meta = ref<null | any>(null);

    const width = ref(0);
    const height = ref(0);

    return {
      meta,
      width,
      height
    };
  },
});
</script>
