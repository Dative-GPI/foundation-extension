<template>
  <FEDialogSubmit
    v-if="dialogId"
    :title="title"
    v-model="dialog"
    v-bind="$attrs"
    @submit="onSubmit"
  >
    <template
      #body
    >
      <FSCalendarTwin
        :color="$props.color"
        v-model="dateRange"
      />
    </template>
  </FEDialogSubmit>
</template>

<script lang="ts">
import { computed, defineComponent, ref, onMounted, watch } from "vue";

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
    const { notify, subscribe } = useExtensionCommunicationBridge();

    const title = ref<string | null>(null);
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
    };

    onMounted(() => {
      subscribe(
        dateRangePayloadSchema,
        location.href,
        (payload: DateRangePayload) => {
          if(payload.dialogId !== dialogId.value) {return;}
          dateRange.value = payload.dateRange; 
        }
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
      title
    };
  }
})
</script>