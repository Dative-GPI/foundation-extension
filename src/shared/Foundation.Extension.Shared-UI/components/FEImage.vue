<template>
  <FSImageUI
    :blurhash="image"
    :source="source"
    @error="onError"
    v-bind="$attrs"
  />
</template>

<script lang="ts">
import { computed, defineComponent, type PropType } from "vue";

import { IMAGE_RAW_SOURCE_URL, IMAGE_THUMBNAIL_SOURCE_URL } from "../config";
import { useImage } from "../composables";

import { useAppAuthToken } from "@dative-gpi/foundation-shared-services/composables";

export default defineComponent({
  name: "FEImage",
  components: {
  },
  props: {
    imageId: {
      type: String as PropType<string | null>,
      required: false,
      default: null
    },
    thumbnail: {
      type: Boolean as PropType<boolean>,
      required: false,
      default: false
    }
  },
  setup(props) {
    const { get: getImage, entity: image } = useImage();
    const { authToken } = useAppAuthToken();

    const source = computed(() => {
      if (props.thumbnail) {
        return (props.imageId && authToken.value) ? IMAGE_THUMBNAIL_SOURCE_URL(props.imageId, authToken.value) : null;
      }
      return (props.imageId && authToken.value) ? IMAGE_RAW_SOURCE_URL(props.imageId, authToken.value) : null;
    });

    const onError = (): void => {
      if (props.imageId) {
        getImage(props.imageId);
      }
    };

    return {
      image,
      source,
      onError
    };
  }
});
</script>