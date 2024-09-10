<template>
  <FEButtonRemoveUI
    v-if="dialogId"
    :dialogPath="DIALOG_REMOVE(dialogId)"
    :removeTotal="$props.removeTotal"
    :removeCurrent="$props.removeCurrent"
    :dialogId="dialogId"
    v-bind="$attrs"
  />
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from "vue";

import { uuidv4 } from "@dative-gpi/bones-ui";

import { DIALOG_REMOVE } from "../config";
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
      DIALOG_REMOVE,
      dialogId
    };
  }
})
</script>