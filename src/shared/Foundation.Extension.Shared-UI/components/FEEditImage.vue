<template>
  <FSEditImageUI
    :blurHash="image"
    :source="source"
    @error="onError"
    @update:source="$emit('update:imageId', $event)"
    v-bind="$attrs"
  />
</template>

<script lang="ts">
import { computed, defineComponent, type PropType } from "vue";

import { IMAGE_RAW_SOURCE_URL } from "../config";
import { useImage } from "../composables";

import { useAppAuthToken } from "@dative-gpi/foundation-shared-services/composables";

export default defineComponent({
  name: "FEEditImage",
  components: {
  },
  props: {
    imageId: {
      type: String as PropType<string | null>,
      required: false,
      default: null
    }
  },
  emits: ["update:imageId"],
  setup(props) {
    const { get: getImage, entity: image } = useImage();
    const { jwt } = useAppAuthToken();

    const source = computed(() => {
      return (props.imageId && jwt.value) ? IMAGE_RAW_SOURCE_URL(props.imageId, jwt.value) : null;
    });

    const onError = (): void => {
      if (props.imageId) {
        getImage(props.imageId);
      }
    };

    return {
      source,
      image,
      onError
    };
  }
});
</script>