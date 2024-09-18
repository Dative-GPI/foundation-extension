<template>
  <v-spacer />
  <FSRow
    :gap="20"
  >
    <FSButton
      variant="standard"
      color="primary"
      :load="synchronizing"
      v-on:click="synchronize(tableId)"
    >
      Synchronize Columns
    </FSButton>
  </FSRow>
</template>

<script lang="ts">
import { defineComponent } from "vue";

import _ from "lodash";

import { useSynchronizeTable } from "../composables";

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
    const { fetch: synchronize, fetching: synchronizing  } = useSynchronizeTable();

    return {
      synchronize,
      synchronizing
    };
  },
});
</script>
