<template>
  <FSButtonRemove
    @click="openDialog(dialogPath)"
    v-bind="$attrs"
  />
</template>

<script lang="ts">
import type { PropType } from "vue";
import { defineComponent, onMounted, watch } from "vue";

import { ColorEnum } from "@dative-gpi/foundation-shared-components/models";
import { useExtensionCommunicationBridge } from "../composables";
import type { DialogReady, DialogSubmit, RemovePayload} from "../domain/dialogs";
import { dialogReadySchema, dialogSubmitSchema } from "../domain/dialogs";

export default defineComponent({
  name: "FEButtonRemoveUI",
  props: {
    dialogPath: {
      type: String,
      required: true
    },
    dialogId: {
      type: String,
      required: true
    },
    error: {
      type: String as PropType<string | null>,
      required: false,
      default: null
    },
    removeTotal: {
      type: Number,
      required: true
    },
    removeCurrent: {
      type: Number,
      required: true
    }
  },
  emits: ["remove"],
  setup(props, { emit }) {
    const { openDialog, subscribe, notify } = useExtensionCommunicationBridge();
    
    const sendRemoveInfos = () => {
      const removeInfosMessage: RemovePayload = {
        messageType: "dialog-remove",
        dialogId: props.dialogId,
        removeTotal: props.removeTotal,
        removeCurrent: props.removeCurrent
      }
      notify(removeInfosMessage);
    };

    const onReceiveDialogReady = (payload: DialogReady) => {
      if (payload.dialogId === props.dialogId) {
        sendRemoveInfos();
      }
    }
    
    const onReceiveDialogSubmit = (payload: DialogSubmit) => {
      if (payload.dialogId === props.dialogId) {
        emit("remove");
      }
    }

    onMounted(() => {
      subscribe(
        dialogReadySchema,
        location.href,
        onReceiveDialogReady
      );

      subscribe(
        dialogSubmitSchema,
        location.href,
        onReceiveDialogSubmit
      )
    });

    watch(() => props.removeCurrent, () => {
      sendRemoveInfos();
    });
    
    return {
      openDialog,
      ColorEnum
    };
  }
})
</script>