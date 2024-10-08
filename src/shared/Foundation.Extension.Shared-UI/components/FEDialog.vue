<template>
  <div
    :class="classes"
    @click="$emit('update:modelValue', false)"
  >
    <FSDialogContent
      height="fit-content"
      :title="$props.title"
      :subtitle="$props.subtitle"
      :width="$props.width"
      :modelValue="true"
      @update:modelValue="$emit('update:modelValue', $event)"
      @click.stop="$emit('click', $event)"
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
</template>

<script lang="ts">
import { computed, defineComponent, onMounted, type PropType, watch } from "vue";

import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui";
import { useBreakpoints } from "@dative-gpi/foundation-shared-components/composables";

import FSDialogContent from "@dative-gpi/foundation-shared-components/components/FSDialogContent.vue";

export default defineComponent({
  name: "FEDialog",
  components: {
    FSDialogContent
  },
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
      type: Number,
      required: false,
      default: 600
    }
  },
  emits: ["click", "update:modelValue"],
  setup(props) {
    const { closeDialog, setDialogWidth, warnDialogMounted } = useExtensionCommunicationBridge();
    const { isExtraSmall } = useBreakpoints();

    const classes = computed((): string[] => {
      const innerClasses: string[] = [];
      if (isExtraSmall.value) {
        innerClasses.push("fe-dialog-wrapper-mobile");
      }
      else {
        innerClasses.push("fe-dialog-wrapper");
      }
      return innerClasses;
    });

    const setWidth = (): void => {
      setDialogWidth(props.width, location.pathname);
    };

    const close = (success: boolean): void => {
      closeDialog(location.pathname, success);
    };

    onMounted((): void => {
      warnDialogMounted(location.pathname);
      setWidth();
    });

    watch(() => props.width, (): void => {
      if (props.width) {
        setWidth();
      }
    }, { immediate: true });

    watch(() => props.modelValue, (): void => {
      if (!props.modelValue) {
        close(true);
      }
    });

    return {
      classes,
      close
    };
  }
});
</script>