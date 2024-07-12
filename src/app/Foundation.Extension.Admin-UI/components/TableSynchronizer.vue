<template>
  <v-spacer />
  <FSRow
    :gap="20"
  >
    <FSButton
      variant="standard"
      color="primary"
      v-on:click="synchronize(tableId)"
    >
      Synchronize Columns
    </FSButton>
  </FSRow>
</template>

<script lang="ts">
import { defineComponent } from "vue";

import _ from "lodash";

import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui";
import { useSynchronizeTable } from "../composables";
import { useRouter } from "vue-router";

export default defineComponent({
  components: {},
  props: {
    editMode: {
      type: Boolean,
      required: true,
    },
    tableId: {
      type: String,
      required: true,
    },
  },
  setup() {
    const extension = useExtensionCommunicationBridge();
    const router = useRouter();
    const { fetch: synchronize, fetching: synchronizing, entity: synchronized  } = useSynchronizeTable();

    return {
      synchronize
    };
  },
});
</script>
