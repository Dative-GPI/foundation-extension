<template>
  <FSDialogContent
    :title="$props.title"
    :subtitle="$props.subtitle"
    :width="$props.width"
    :modelValue="true"
    @update:modelValue="$emit('update:modelValue', $event)"
  >
    <template
      v-for="(_, name) in $slots"
      v-slot:[name]="slotData"
    >
      <slot
        :name="name"
        v-bind="slotData"
      />
    </template>
  </FSDialogContent>
</template>

<script lang="ts">
import { defineComponent, watch, onMounted } from "vue";
import type { PropType } from "vue";

import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui";
import FSDialogContent from "@dative-gpi/foundation-shared-components/components/FSDialogContent.vue";

export default defineComponent({
  name: "FEDialog",
  components: {
    FSDialogContent
  },
  props: {
    modelValue: {
      type: Boolean,
      required: true,
    },
    title: {
      type: String as PropType<string | null>,
      required: false,
      default: null
    },
    subtitle: {
      type: String as PropType<string | null>,
      required: false,
      default: null
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
      extension.closeDialog(location.pathname, success);
    };

    onMounted(() => {
      setWidth();
      setHeight();
    });

    watch(
      () => props.width,
      (newVal) => {
        if (newVal) {
          setWidth();
        }
      }
    );

    watch(
      () => props.height,
      (newVal) => {
        if (newVal) {
          setHeight();
        }
      }
    );

    watch(
      () => props.modelValue,
      (newVal) => {
        if (newVal == false) {
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
