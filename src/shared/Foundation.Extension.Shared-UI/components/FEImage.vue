<template>
  <FSImageUI
    :blurhash="blurHash"
    :source="source"
    @error="onError"
    v-bind="$attrs"
  />
</template>

<script lang="ts">
import { computed, defineComponent, type PropType } from "vue";

import { IMAGE_RAW_SOURCE_URL } from "../config";

import { useImageBlurHash, useExtensionJwt } from "@dative-gpi/foundation-shared-services/composables";

export default defineComponent({
  name: "FEImage",
  components: {
  },
  props: {
    imageId: {
      type: String as PropType<string | null>,
      required: false,
      default: null
    }
  },
  setup(props) {
    const { fetch: fetchBlurHash, entity: blurHash } = useImageBlurHash();
    const { jwt } = useExtensionJwt();

    const source = computed(() => {
      return (props.imageId && jwt.value) ? IMAGE_RAW_SOURCE_URL(props.imageId, jwt.value) : null;
    });

    const onError = (): void => {
      if (props.imageId) {
        fetchBlurHash(props.imageId);
      }
    };

    return {
      blurHash,
      source,
      onError
    };
  }
});
</script>