<template>
  <FEDialog
    :subtitle="$props.subtitle"
    :title="$props.title"
    :width="$props.width"
    :modelValue="$props.modelValue"
    @update:modelValue="$emit('update:modelValue', $event)"
    v-bind="$attrs"
  >
    <template
      #body
    >
      <FSFadeOut
        padding="0 8px 0 0"
        :maxHeight="maxHeight"
      >
        <slot
          name="body"
        />
      </FSFadeOut>
    </template>
    <template
      #footer
    >
      <slot
        name="footer"
      >
        <FSRow
          padding="0 16px 0 0"
        >
          <slot
            name="left-footer"
          />
          <FSRow
            align="top-right"
            :wrap="false"
          >
            <FSButton
              :prependIcon="$props.cancelButtonPrependIcon"
              :appendIcon="$props.cancelButtonAppendIcon"
              :variant="$props.cancelButtonVariant"
              :color="$props.cancelButtonColor"
              :label="cancelLabel"
              @click="$emit('update:modelValue', false)"
            />
            <FSButton
              :prependIcon="$props.submitButtonPrependIcon"
              :appendIcon="$props.submitButtonAppendIcon"
              :variant="$props.submitButtonVariant"
              :color="$props.submitButtonColor"
              :editable="$props.editable"
              :label="submitLabel"
              :load="$props.load"
              @click="onSubmit"
            />
          </FSRow>
        </FSRow>
      </slot>
    </template>
  </FEDialog>
</template>

<script lang="ts">
import { computed, defineComponent, type PropType } from "vue";

import { useTranslations as useTranslationsProvider } from "@dative-gpi/bones-ui/composables";
import { type ColorBase, ColorEnum } from "@dative-gpi/foundation-shared-components/models";
import { useBreakpoints } from "@dative-gpi/foundation-shared-components/composables";

import { useExtensionCommunicationBridge } from "../composables";
import type { DialogSubmit } from "../domain/dialogs";

import FEDialog from "./FEDialog.vue";

export default defineComponent({
  name: "FEDialogSubmit",
  components: {
    FEDialog
  },
  props: {
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
      required: false
    },
    modelValue: {
      type: Boolean,
      required: true
    },
    cancelButtonPrependIcon: {
      type: String as PropType<string | null>,
      required: false,
      default: null
    },
    cancelButtonLabel: {
      type: String as PropType<string | null>,
      required: false,
      default: null
    },
    cancelButtonAppendIcon: {
      type: String as PropType<string | null>,
      required: false,
      default: null
    },
    cancelButtonVariant: {
      type: String as PropType<"standard" | "full" | "icon">,
      required: false,
      default: "standard"
    },
    cancelButtonColor: {
      type: String as PropType<ColorBase>,
      required: false,
      default: ColorEnum.Light
    },
    submitButtonPrependIcon: {
      type: String as PropType<string | null>,
      required: false,
      default: null
    },
    submitButtonLabel: {
      type: String as PropType<string | null>,
      required: false,
      default: null
    },
    submitButtonAppendIcon: {
      type: String as PropType<string | null>,
      required: false,
      default: null
    },
    submitButtonVariant: {
      type: String as PropType<"standard" | "full" | "icon">,
      required: false,
      default: "full"
    },
    submitButtonColor: {
      type: String as PropType<ColorBase>,
      required: false,
      default: ColorEnum.Primary
    },
    load: {
      type: Boolean,
      required: false,
      default: false
    },
    editable: {
      type: Boolean,
      required: false,
      default: true
    }
  },
  emits: ["update:modelValue", "submit"],
  setup(props, { emit }) {
    const { notify } = useExtensionCommunicationBridge();
    const { isMobileSized } = useBreakpoints();
    const { $tr } = useTranslationsProvider();

    const dialogId = computed(() => {
      return new URL(window.location.toString()).searchParams.get("dialogId");
    });


    const maxHeight = computed(() => {
      const other = 24 + 24                                          // Paddings
        + (isMobileSized.value ? 24 : 32) + 24                       // Title
        + (props.subtitle ? (isMobileSized.value ? 14 : 16) + 8 : 0) // Subtitle
        + (isMobileSized.value ? 36 : 40) + 24;                      // Footer
      return `calc(100vh - 40px - ${other}px)`;
    });

    const cancelLabel = computed(() => {
      return props.cancelButtonLabel ?? $tr("ui.button.cancel", "Cancel");
    });

    const submitLabel = computed(() => {
      return props.submitButtonLabel ??  $tr("ui.button.validate", "Validate");
    });

    const onSubmit = () => {
      if(!dialogId.value) {return;}
      const submitMessage: DialogSubmit = {
        messageType: "dialog-submit",
        dialogId: dialogId.value
      };
      notify(submitMessage);
      emit("submit");
    };

    return {
      cancelLabel,
      submitLabel,
      ColorEnum,
      maxHeight,
      onSubmit
    };
  }
});
</script>