<template>
  <FEButtonRemoveUI
    v-if="dialogId"
    :dialogPath="REMOVE_ENTITIES_DIALOG_PATH(dialogId)"
    :removeTotal="$props.removeTotal"
    :removeCurrent="$props.removeCurrent"
    :dialogId="dialogId"
    v-bind="$attrs"
  />
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from "vue";

import { uuidv4 } from "@dative-gpi/bones-ui";

import { REMOVE_ENTITIES_DIALOG_PATH } from "../config";
import FEButtonRemoveUI from "@dative-gpi/foundation-extension-shared-ui/components/FEButtonRemoveUI.vue";

export default defineComponent({
  name: "FEButtonRemove",
  components: {
    FEButtonRemoveUI
  },
  props: {
    removeTotal: {
      type: Number,
      required: true
    },
    removeCurrent: {
      type: Number,
      required: false,
      default: 0
    }
  },
  setup() {
    const dialogId = ref<string | null>(null);

    onMounted(() => {
      dialogId.value = uuidv4();
    });

    return {
      REMOVE_ENTITIES_DIALOG_PATH,
      dialogId
    };
  }
})
</script>