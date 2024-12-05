<template>
  <FEDialogSubmit
    v-if="dialogId"
    :title="title"
    :submitButtonLabel="$tr('ui.common.remove', 'Remove')"
    :submitButtonColor="ColorEnum.Error"
    @submit="removing = true"
    v-model="dialog"
    v-bind="$attrs"
  >
    <template
      #body
    >
      <FSCol
        gap="16px"
      >
        <FSRow>
          <FSIcon
            :color="ColorEnum.Error"
          >
            mdi-alert-outline
          </FSIcon>
          <FSRow
            gap="4px"
          >
            <FSSpan>
              {{ $tr("remove-dialog.body-regular", "This action is") }}
            </FSSpan>
            <FSSpan
              font="text-button"
            >
              {{ $tr("remove-dialog.body-bold", "definitive") }}
            </FSSpan>
          </FSRow>
        </FSRow>
        <FSSpan>
          {{ $tr("remove-dialog.final-warning", "Once removed, entities won't be retrievable") }}
        </FSSpan>
      </FSCol>
    </template>
    <template
      #footer
      v-if="removing"
    >
      <FSRow
        padding="0 16px 0 0"
        align="center-right"
        :height="footerHeight"
      >
        <FSSpan>
          {{ removeCurrent }} / {{ removeTotal }}
        </FSSpan>
        <v-progress-circular
          :color="ColorEnum.Dark"
          :indeterminate="true"
        />
      </FSRow>
    </template>
  </FEDialogSubmit>
</template>

<script lang="ts">
import { computed, defineComponent, ref, onMounted, watch } from "vue";

import { useTranslations as useTranslationsProvider } from "@dative-gpi/bones-ui/composables";
import { useBreakpoints } from "@dative-gpi/foundation-shared-components/composables";
import { ColorEnum } from "@dative-gpi/foundation-shared-components/models";

import { useExtensionCommunicationBridge } from "../composables";

import FEDialogSubmit from "./FEDialogSubmit.vue";
import type { DialogReady, RemovePayload} from "../domain/dialogs";
import { removePayloadSchema } from "../domain/dialogs";

export default defineComponent({
  name: "FEDialogRemove",
  components: {
    FEDialogSubmit
  },
  setup() {
    const { notify, subscribe } = useExtensionCommunicationBridge();
    const { isMobileSized } = useBreakpoints();
    const { $tr } = useTranslationsProvider();

    const dialog = ref(true);
    const removeTotal = ref(0);
    const removeCurrent = ref(0);
    const removing = ref(false);

    const dialogId = computed(() => {
      return new URL(window.location.toString()).searchParams.get("dialogId");
    });

    const title = computed((): string => {
      if (removeTotal.value > 1) {
        return $tr("dialog-remove.remove-plural", "Remove {0} entities", removeTotal.value.toString());
      }
      else {
        return $tr("dialog-remove.remove-singular", "Remove an entity");
      }
    });

    const footerHeight = computed((): string => {
      return isMobileSized.value ? "36px" : "40px";
    });

    onMounted(() => {
      subscribe(
        removePayloadSchema,
        location.href,
        (payload: RemovePayload) => {
          if(payload.dialogId !== dialogId.value) {return;}
          removeTotal.value = payload.removeTotal;
          removeCurrent.value = payload.removeCurrent;
          if(removeCurrent.value === removeTotal.value) {
            dialog.value = false;
          }
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
      removeCurrent,
      footerHeight,
      removeTotal,
      ColorEnum,
      removing,
      dialogId,
      dialog,
      title
    };
  }
})
</script>