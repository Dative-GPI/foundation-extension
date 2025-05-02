<template>
  <FSCol
    height="100%"
    :padding="12"
  >
    <FSText
      v-if="$props.meta"
      font="text-h2"
    >
      {{ $props.meta.label }}
    </FSText>
    <FSText
      v-if="$props.width && $props.height"
      font="text-body"
    >
      {{ `I am a ${$props.width} x ${$props.height} widget.` }}
    </FSText>
  </FSCol>
</template>

<script lang="ts">
import { defineComponent, type PropType } from "vue";

import type { ExampleMeta } from '@/models';

export default defineComponent({
  name: "TestWidget",
  components: {
  },
  props: {
    meta: {
      type: Object as PropType<ExampleMeta | null>,
      required: true
    },
    width: {
      type: Number as PropType<number | null>,
      required: true
    },
    height: {
      type: Number as PropType<number | null>,
      required: true
    }
  },
  setup() {

    return {
    };
  },
});
</script>
