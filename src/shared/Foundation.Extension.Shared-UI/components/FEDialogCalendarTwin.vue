<template>
  <FEDialogSubmit
    v-if="dialogId"
    :width="700"
    :title="title"
    v-model="dialog"
    v-bind="$attrs"
    @submit="onSubmit"
  >
    <template
      #body
    >
      <FSCalendarTwin
        :color="color"
        v-model="dateRange"
      />
    </template>
  </FEDialogSubmit>
</template>

<script lang="ts">
import { computed, defineComponent, ref, onMounted, watch } from "vue";
import Ajv from "ajv";

import { ColorEnum } from "@dative-gpi/foundation-shared-components/models";

import { useExtensionCommunicationBridge } from "../composables";

import FEDialogSubmit from "./FEDialogSubmit.vue";
import type { DateRangePayload, DialogReady, SubmitDateRange} from "../domain/dialogs";
import { dateRangePayloadSchema } from "../domain/dialogs";

export default defineComponent({
  name: "FEDialogRemove",
  components: {
    FEDialogSubmit
  },
  setup() {
    const { notify, subscribeUnsafe } = useExtensionCommunicationBridge();

    const title = ref<string | null>(null);
    const color = ref<string | ColorEnum>(ColorEnum.Primary);
    const dateRange = ref<number[] | null>(null);
    const dialog = ref(true);

    const dialogId = computed(() => {
      return new URL(window.location.toString()).searchParams.get("dialogId");
    });

    const onSubmit = () => {
      if(!dialogId.value) {return;}
      let message: SubmitDateRange = {
        messageType: "dialog-submit-date-range",
        dialogId: dialogId.value,
        dateRange: dateRange.value
      }
      notify(message);
      dialog.value = false;
    };

    onMounted(() => {
      subscribeUnsafe(
        location.href,
        (payload: DateRangePayload) => {
          if(payload.dialogId !== dialogId.value) {return;}
          dateRange.value = payload.dateRange;
          if(payload.title) {
            title.value = payload.title;
          }
          if(payload.color) {
            color.value = payload.color;
          }
        },
        new Ajv().compile(dateRangePayloadSchema)
      );
    })

    watch(() => dialogId.value, () => {
      if(!dialogId.value) {return;}
      let message: DialogReady = {
        messageType: "dialog-ready",
        dialogId: dialogId.value
      }
      notify(message);
    }, { immediate: true });
    
    return {
      ColorEnum,
      dateRange,
      onSubmit,
      dialogId,
      dialog,
      color,
      title
    };
  }
})
</script>