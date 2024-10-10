<template>
  <div
    :class="classes"
    @click="$emit('update:modelValue', false)"
  >
    <FSDialogContent
      class="fe-dialog-content"
      height="fit-content"
      ref="dialogContent"
      :title="$props.title"
      :subtitle="$props.subtitle"
      :width="$props.width"
      :style="style"
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
import { computed, defineComponent, onMounted, type PropType, ref, type StyleValue, watch } from "vue";

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
      type: [Array, String, Number] as PropType<string[] | number[] | string | number>,
      required: false,
      default: "600px"
    }
  },
  emits: ["click", "update:modelValue"],
  setup(props) {
    const { closeDialog, setDialogHeight, setDialogMounted, setDialogWidth,  } = useExtensionCommunicationBridge();
    const { isExtraSmall } = useBreakpoints();

    const dialogContent = ref<HTMLElement | null>(null);
    const mounted = ref(false);

    const style = computed((): StyleValue => ({
      "--fe-dialog-opacity": mounted.value ? 1 : 0
    }));

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

    const close = (success: boolean): void => {
      closeDialog(location.pathname, success);
    };

    onMounted((): void => {
      if (dialogContent.value) {
        setDialogHeight((dialogContent.value as any).$el.clientHeight, location.pathname);
      }
      if (props.width) {
        setDialogWidth(props.width, location.pathname);
      }
      setDialogMounted(location.pathname);
      setTimeout(() => {
        mounted.value = true;
      }, 280);
    });

    watch(() => props.width, (): void => {
      if (props.width) {
        setDialogWidth(props.width, location.pathname);
      }
    }, { immediate: true });

    watch(() => props.modelValue, (): void => {
      if (!props.modelValue) {
        close(true);
      }
    });

    return {
      dialogContent,
      classes,
      style,
      close
    };
  }
});
</script>