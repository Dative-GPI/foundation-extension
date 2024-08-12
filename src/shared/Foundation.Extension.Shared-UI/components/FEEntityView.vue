<template>
  <FSEntityViewUI
    :imageSource="source"
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
  </FSEntityViewUI>
</template>

<script lang="ts">
import { computed, defineComponent, type PropType } from "vue";

import FSEntityViewUI from "@dative-gpi/foundation-shared-components/components/views/FSEntityViewUI.vue";
import { useExtensionJwt } from "@dative-gpi/foundation-shared-services/composables";

import { IMAGE_RAW_SOURCE_URL } from "../config";

export default defineComponent({
  name: "FEEntityView",
  components: {
    FSEntityViewUI
  },
  props: {
    imageId: {
      type: String as PropType<string | null>,
      required: false,
      default: null
    }
  },
  setup(props) {
    const { jwt } = useExtensionJwt();

    const source = computed(() => {
      return (props.imageId && jwt.value) ? IMAGE_RAW_SOURCE_URL(props.imageId, jwt.value) : null;
    });

    return {
      source
    };
  }
});
</script>