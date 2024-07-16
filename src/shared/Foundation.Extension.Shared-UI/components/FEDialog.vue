<template>
  <FSCol>
    <slot></slot>
  </FSCol>
</template>

<script lang="ts">
import { defineComponent, watch, onMounted } from "vue";

import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui";

export default defineComponent({
  name: "FEDialog",
  props: {
    value: {
      type: Boolean,
      required: true,
    },
    height: {
      type: Number,
      required: false,
      default: 400
    },
    width: {
      type: Number,
      required: false,
      default: 800
    },
  },
  setup(props) {
    const extension = useExtensionCommunicationBridge();

    const setWidth = () => {
      extension.setDialogWidth(props.width, location.pathname);
    };

    const setHeight = () => {
      extension.setDialogHeight(props.height, location.pathname);
    };

    const close = (success: boolean) => {
      extension.closeDrawer(location.pathname, success);
    };

    onMounted(() => {
      setWidth();
      setHeight();
    });

    watch(
      () => props.width,
      (value) => {
        if (value) {
          setWidth();
        }
      }
    );

    watch(
      () => props.height,
      (value) => {
        if (value) {
          setHeight();
        }
      }
    );

    watch(
      () => props.value,
      (value) => {
        if (value) {
          close(true);
        }
      }
    );

    return {
      close,
    };
  },
});
</script>

<style scoped></style>
