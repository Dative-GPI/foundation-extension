<template>
  <FEDateRangeFieldUI
    v-if="dialogId"
    :dialogPath="CALENDAR_TWIN_DIALOG(dialogId)"
    :dialogId="dialogId"
    v-bind="$attrs"
  />
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from "vue";

import { uuidv4 } from "@dative-gpi/bones-ui";

import { CALENDAR_TWIN_DIALOG } from "../config";
import FEDateRangeFieldUI from "@dative-gpi/foundation-extension-shared-ui/components/FEDateRangeFieldUI.vue";

export default defineComponent({
  name: "FEDateRangeField",
  components: {
    FEDateRangeFieldUI
  },
  setup() {
    const dialogId = ref<string | null>(null);

    onMounted(() => {
      dialogId.value = uuidv4();
    });

    return {
      CALENDAR_TWIN_DIALOG,
      dialogId
    };
  }
})
</script>