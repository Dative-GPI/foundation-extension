<template>
  <FEDialog
    title="Example Dialog"
    subtitle="Example Dialog Subtitle"
    :width="600"
    v-model="dialog"
  >
    <template
      #body
    >
      <FSDialogFormBody
        :subtitle="'something'"
        @click:submitButton="onSubmit"
        @click:cancelButton="dialog = false"
        v-bind="$attrs"
      >
        <template
          #body
        >
          <FSCol
            gap="12px"
          >
            <FSText>
              {{ $tr('ui.common.information','Information') }}
            </FSText>
            <FSTranslateField
              :label="$tr('ui.chart-organisation.label-default','Name')"
              :required="true"
              :rules="[TextRules.required()]"
              v-model="fieldValue"
            />
            <FSTranslateField
              :label="$tr('ui.example.label','Label')"
              :required="true"
              :rules="[TextRules.required()]"
              :modelValue="labelDefault"
              :translations="translations"
            />
            {{ timeZone ?? 'No time zone' }}
          </FSCol>
        </template>
      </FSDialogFormBody>
    </template>
  </FEDialog>
</template>

<script lang="ts">
import { defineComponent,ref } from "vue";

import { TextRules } from "@dative-gpi/foundation-shared-components/models";
import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui/composables";
import { useAppTimeZone } from "@dative-gpi/foundation-shared-services/composables";


import FEDialog from "@dative-gpi/foundation-extension-shared-ui/components/FEDialog.vue";

export default defineComponent({
  name: "ExampleDialog",
  components: {
    FEDialog
  },
  setup() {
    const { notify } = useExtensionCommunicationBridge();
    const fieldValue = ref("");
    const dialog = ref(true);

    const labelDefault = ref("");
    const translations = ref([]); 

    const { timeZone } = useAppTimeZone();

    const onSubmit = () => {
      let program = {
        messageType: "stepProgram",
        stepNumber: undefined,
        program: {
          createId: "uuidv4()",
          modelCode: "currentModel.value!.code",
          optionIds: "electedOptionIds.value",
          optionCodes: "selectedOptionCodes.value",
          duration: "duration.value",
          informations: "informations.value",
          phases: "phases.value"
        }
      };

      console.log("notif")
      notify(program);
      console.log("Good job you submitted your first extension form !");
      dialog.value = false;
    };
    
    return {
      labelDefault,
      translations,
      fieldValue,
      TextRules,
      dialog,
      onSubmit,
      timeZone
    };
  },
});
</script>