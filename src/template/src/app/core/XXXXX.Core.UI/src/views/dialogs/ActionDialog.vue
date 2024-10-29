<template>
  <FEDialog
    :title="$tr('ui.common.action', 'Action')"
    :width="736"
    :height="500"
    v-model="dialog"
  >
    <template
      #body
    >
      <FSDialogFormBody
        @click:cancelButton="dialog = false"
        @click:submitButton="onSubmit"
      >
        <template
          #body
        >
          <FSCol
            gap="24px"
          >
            <FSText
              :lineClamp="5"
            >
              {{ $tr('ui.common.description', 'Description of this dialog action') }}
            </FSText>
            <FSTranslateField />
            <FSCol
              v-if="legalInformation"
            >
              <FSButton
                :label="$tr('ui.common.general-conditions-of-use', 'General conditions of use')"
                @click="downloadFile(legalInformation.generalConditionsId)"
              />
              <FSButton
                :label="$tr('ui.common.privacy-policy', 'Privacy policy')"
                @click="downloadFile(legalInformation.privacyPolicyId)"
              />
            </FSCol>
            <FSErrorToast
              v-if="error"
              :errorCode="error"
            />
          </FSCol>
        </template>
      </FSDialogFormBody>
    </template>
  </FEDialog>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from "vue";

import { useCurrentLegalInformation, useFiles } from "@dative-gpi/foundation-shared-services/composables";

import FEDialog from "@dative-gpi/foundation-extension-shared-ui/components/FEDialog.vue";

export default defineComponent({
  name: "CreateRegistrationDialog",
  components: {
    FEDialog
  },
  setup() {
    const { fetch: getLegalInformation, entity: legalInformation } = useCurrentLegalInformation();
    const { downloadFile } = useFiles();
    
    const error = ref<string | null>(null);
    const dialog = ref(true);

    const onSubmit = async () => {
      try {
        error.value = null;
        dialog.value = false;
      }
      catch (exception: any) {
        error.value = exception.response.data;
      }
    };

    onMounted(() => {
      getLegalInformation();
    });

    return {
      legalInformation,
      dialog,
      error,
      downloadFile,
      onSubmit
    };
  }
});
</script>