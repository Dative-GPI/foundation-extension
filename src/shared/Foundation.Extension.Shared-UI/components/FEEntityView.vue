<template>
  <FSEntityView
    :imageCover="$props.imageCover"
    :imageId="$props.imageId"
    v-bind="$attrs"
  >
    <template
      v-for="(_, name) in $slots"
      v-slot:[name]="slotData"
    >
      <slot
        :name="name"
        v-bind="{ ...slotData }"
      />
    </template>
    <template
      #image="{ actualImageSize }"
    >
      <FEImage
        :imageId="$props.imageId"
        :cover="$props.imageCover"
        :imageB64="$props.imageB64"
        :height="actualImageSize"
        :width="actualImageSize"
      />
    </template>
  </FSEntityView>
</template>

<script lang="ts">
import { defineComponent, type PropType } from "vue";

import FSEntityView from "@dative-gpi/foundation-shared-components/components/views/FSEntityView.vue";
import FEImage from "./FEImage.vue";

export default defineComponent({
  name: "FEEntityView",
  components: {
    FSEntityView,
    FEImage
  },
  props: {
    imageId: {
      type: String as PropType<string | null>,
      required: false,
      default: null
    },
    imageCover: {
      type: Boolean,
      required: false,
      default: true
    },
    imageB64: {
      type: String as PropType<string | null>,
      required: false,
      default: null
    }
  },
  setup() {
  }
});
</script>