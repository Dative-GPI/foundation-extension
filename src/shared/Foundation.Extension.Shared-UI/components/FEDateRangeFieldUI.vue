<template>
  <FSTextField
    class="fs-date-field"
    :label="$props.label"
    :description="$props.description"
    :color="$props.color"
    :hideHeader="$props.hideHeader"
    :required="$props.required"
    :editable="$props.editable"
    :readonly="true"
    :rules="$props.rules"
    :messages="messages"
    :validateOn="validateOn"
    :validationValue="$props.modelValue"
    :modelValue="toShortDateFormat"
    @click="onClick"
    @update:modelValue="onClear"
  >
    <template
      #prepend-inner
    >
      <slot
        name="prepend-inner"
      >
        <FSButton
          variant="icon"
          icon="mdi-calendar"
          :editable="$props.editable"
          :color="ColorEnum.Dark"
        />
      </slot>
    </template>
    <template
      v-for="(_, name) in $slots"
      v-slot:[name]="slotData"
    >
      <slot
        :name="name"
        v-bind="slotData"
      />
    </template>
  </FSTextField>
</template>

<script lang="ts">
import { computed, defineComponent, type PropType, watch, onMounted } from "vue";
import _ from "lodash";
import Ajv from "ajv";

import { ColorEnum } from "@dative-gpi/foundation-shared-components/models";
import { useDateFormat } from "@dative-gpi/foundation-shared-services/composables";
import { useRules } from "@dative-gpi/foundation-shared-components/composables";
import { useExtensionCommunicationBridge } from "../composables";
import type { DateRangePayload, DialogReady, SubmitDateRange } from "../domain/dialogs";
import { dialogReadySchema, submitDateRangeSchema } from "../domain/dialogs";

export default defineComponent({
  name: "FEDateRangeFieldUI",
  components: {
  },
  props: {
    dialogPath: {
      type: String,
      required: true
    },
    dialogId: {
      type: String,
      required: true
    },
    title: {
      type: String as PropType<string | null>,
      required: false,
      default: null
    },
    color: {
      type: String as PropType<string | null>,
      required: false,
      default: null
    },
    label: {
      type: String as PropType<string | null>,
      required: false,
      default: null
    },
    description: {
      type: String as PropType<string | null>,
      required: false,
      default: null
    },
    modelValue: {
      type: Array as PropType<number[] | null>,
      required: false,
      default: null
    },
    hideHeader: {
      type: Boolean,
      required: false,
      default: false
    },
    required: {
      type: Boolean,
      required: false,
      default: false
    },
    rules: {
      type: Array as PropType<any[]>,
      required: false,
      default: () => []
    },
    editable: {
      type: Boolean,
      required: false,
      default: true
    }
  },
  emits: ["update:modelValue"],
  setup(props, { emit }) {
    const { validateOn, getMessages } = useRules();
    const { epochToShortDateFormat } = useDateFormat();
    const { openDialog, subscribe, subscribeUnsafe, notify } = useExtensionCommunicationBridge();

    const toShortDateFormat = computed((): string => {
      if (!props.modelValue || !Array.isArray(props.modelValue) || !props.modelValue.length) {
        return "";
      }
      return props.modelValue.map((epoch) => epochToShortDateFormat(epoch)).join(" - ");
    });

    const messages = computed((): string[] => getMessages(props.modelValue, props.rules, true));

    const onClick = (): void => {
      if (props.editable) {
        openDialog(props.dialogPath);
      }
    };

    const onClear = (): void => {
      emit("update:modelValue", null);
    };
    const sendDateRangeInfos = () => {
      const dateRangeInfosMessage: DateRangePayload = {
        messageType: "dialog-date-range",
        dialogId: props.dialogId,
        dateRange: props.modelValue,
        title: props.title,
        color: props.color
      }
      notify(dateRangeInfosMessage);
    };
    
    const onReceiveDialogReady = (payload: DialogReady) => {
      if (payload.dialogId === props.dialogId) {
        sendDateRangeInfos();
      }
    }
    
    const onReceiveDialogSubmit = (payload: SubmitDateRange) => {
      if (payload.dialogId === props.dialogId) {
        emit("update:modelValue", payload.dateRange);
      }
    }

    onMounted(() => {
      subscribe(
        dialogReadySchema,
        location.href,
        onReceiveDialogReady
      );

      subscribeUnsafe(
        location.href,
        onReceiveDialogSubmit,
        new Ajv().compile(submitDateRangeSchema)
      )
    });

    watch(() => props.modelValue, (newValue, oldValue) => {
      if (!_.isEqual(newValue, oldValue)) {
        sendDateRangeInfos();
      }
    });

    return {
      toShortDateFormat,
      validateOn,
      ColorEnum,
      messages,
      onClick,
      onClear
    };
  }
});
</script>