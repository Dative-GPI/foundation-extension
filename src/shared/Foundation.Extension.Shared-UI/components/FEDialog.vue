<template>
  <FSDialog
    :modelValue="$props.modelValue"
    @update:modelValue="$emit('update:modelValue', false)"
    :width="$props.width"
  >
    <div
      ref="element"
    >
      <FSDialogContent
        class="fe-dialog-content"
        height="fit-content"
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
    </div>
  </FSDialog>
</template>

<script lang="ts">
import { defineComponent, type PropType, ref, watch } from "vue";

import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui";
import { useBreakpoints } from "@dative-gpi/foundation-shared-components/composables";

export default defineComponent({
  name: "FEDialog",
  props: {
    modelValue: {
      type: Boolean,
      required: true
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
    width: {
      type: [Array, String, Number] as PropType<string[] | number[] | string | number>,
      required: false,
      default: "600px"
    }
  },
  emits: ["click", "update:modelValue"],
  setup(props) {
    const { closeDialog, setDialogHeight, setDialogMounted, setDialogWidth,  } = useExtensionCommunicationBridge();
    const { isExtraSmall } = useBreakpoints();

    const element = ref<HTMLElement | null>(null);

    const close = (success: boolean): void => {
      closeDialog(location.pathname, success);
    };

    watch(() => props.width, (): void => {
      if (props.width) {
        setDialogWidth(props.width, location.pathname);
      }
    });

    watch(() => props.modelValue, (): void => {
      if (!props.modelValue) {
        close(true);
      }
    });

    watch(element, (): void => {
      if (element.value) {
        setDialogHeight(element.value.clientHeight, location.pathname);
        setDialogWidth(element.value.clientWidth, location.pathname);
        setDialogMounted(location.pathname);
      }
    });

    return {
      isExtraSmall,
      element,
      close
    };
  }
});
</script>