<template>
  <FSCol
    height="100%"
    :padding="12"
    gap="24"
  >
    <FSCol
      gap="6"
    >
      <FSText
        v-if="$props.meta"
        font="text-h2"
      >
        {{ $props.meta.label }}
      </FSText>
      <FSText
        v-if="$props.meta"
        font="text-overline"
      >
        {{ `I am a ${ $props.widgetWidth } x ${ $props.widgetHeight } widget` }}
      </FSText>
    </FSCol>
    <FSCol
      gap="6"
    >
      <FSText
        v-if="$props.settings"
        font="text-button"
      >
        {{ `Dashboard settings :` }}
      </FSText>
      <FSText
        v-if="$props.settings"
        lineClamp="8"
        font="text-body"
      >
      {{ JSON.stringify($props.settings) }}
      </FSText>
    </FSCol>
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
    settings: {
      type: Object as PropType<Object | null>,
      required: true
    },
    widgetWidth: {
      type: Number,
      required: true
    },
    widgetHeight: {
      type: Number,
      required: true
    }
  },
  setup() {

    return {
    };
  },
});
</script>
