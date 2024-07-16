<template>
  <FSCol
    :gap="24"
  >
    <FSSpan
      font="text-h2"
    >{{ $tr("ui.extension.title", "Title") }}</FSSpan>
    <FSSpan
      font="text-body"
    >{{ $tr("ui.extension.body", "Body") }}</FSSpan>
    <FSSpan
      font="text-button"
    >{{ $tr("ui.commmon.label", "Label") }}</FSSpan>
    <FSButton
      label="open a dialog"
      @click="openDialog('organisations/41ea29a1-bbfe-4f4a-86b3-425963471053/XXXXX/examples/drawer')"
    />
    <FSSpan
      font="text-h1"
    >Table Test</FSSpan>
    <FEDataTable
      :tableCode="tableCode"
      :items="items"
    />


  </FSCol>
</template>

<script lang="ts">
import { defineComponent } from "vue";

import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui/composables";
import FEDataTable from "@dative-gpi/foundation-extension-core-ui/components/FEDataTable.vue";

export default defineComponent({
  name: "ExampleComponent",
  components: {
    FEDataTable
  },
  setup() {
    const { openDialog } = useExtensionCommunicationBridge();

    const tableCode = "ui.tables.test";

    const items = [
      {
        id: 1,
        name: "Item 1",
        description: "Description 1",
      },
      {
        id: 2,
        name: "Item 2",
        description: "Description 2",
      },
      {
        id: 3,
        name: "Item 3",
        description: "Description 3",
      },
    ];

    return {
      tableCode,
      items,
      openDialog
    };
  },
});
</script>

<style scoped></style>
