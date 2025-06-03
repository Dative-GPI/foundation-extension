<template>
  <FSTextField
    v-if="$props.meta"
    label="Label"
    :modelValue="$props.meta.label"
    @update:modelValue="$emit('update:meta', {...$props.meta, label: $event})"
  />

  Dashboard settings: 
  {{ $props.settings }}
</template>

<script lang="ts">
import { defineComponent, type PropType } from "vue";

import type { ExampleMeta } from '@/models';

export default defineComponent({
  name: "TestWidgetConfiguration",
  props: {
    meta: {
      type: Object as PropType<ExampleMeta | null>,
      required: true
    },
    settings: {
      type: Object,
      required: false,
      default: () => ({})
    }
  },
  emits: ['update:meta'],
  setup() {  

    return {
    };
  },
});
</script>
