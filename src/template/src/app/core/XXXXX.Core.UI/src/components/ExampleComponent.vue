<template>
  <FSCol
    :gap="24"
  >
    <FSButton
      label="open a dialog"
      @click="openDialog('organisations/41ea29a1-bbfe-4f4a-86b3-425963471053/XXXXX/examples/dialog?height=[282, 258]')"
    />
    <FSSpan
      font="text-h1"
    >
      Table Test
    </FSSpan>
    <FEDataTable
      :tableCode="tableCode"
      :items="items"
      v-model="selectedItems"
    />
    Selected elements : {{ selectedItems.join(", ") }}
    <FEButtonRemove
      :removeTotal="1"
      :removeCurrent="fakeRemove"
      @remove="fakeRemove = 8"
    />
    <FEDateRangeField
      :title="$tr('ui.code','select a date range')"
      :color="ColorEnum.Success"
      v-model="dateRange"
    />
    <FSRow
      height="1000px"
      style="background-color: brown;"
    >
      1000px row for scroll
    </FSRow>
  </FSCol>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from "vue";
import Ajv from "ajv";

import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui/composables";
import { ColorEnum } from "@dative-gpi/foundation-shared-components/models";

import FEDateRangeField from "@dative-gpi/foundation-extension-core-ui/components/FEDateRangeField.vue";
import FEButtonRemove from "@dative-gpi/foundation-extension-core-ui/components/FEButtonRemove.vue";
import FEDataTable from "@dative-gpi/foundation-extension-core-ui/components/FEDataTable.vue";

export default defineComponent({
  name: "ExampleComponent",
  components: {
    FEDateRangeField,
    FEButtonRemove,
    FEDataTable
  },
  setup() {
    const { openDialog, subscribeUnsafe } = useExtensionCommunicationBridge();

    const fakeRemove = ref(0);
    const selectedItems = ref<string[]>([]);
    const dateRange = ref<number[] | null>([ 1729029600000, 1729288800000 ]);

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

    let programSchema = {
      type: "object",
      properties: {
        messageType: { enum: ["stepProgram"] },
        stepNumber: { type: "string" },
        program: { type: "object" },
      },
      required: ["messageType", "program"],
    };

    onMounted(() => {
      subscribeUnsafe(
        location.href,
        (payload: any) => {
          console.trace()
          console.log(payload)
        },
        new Ajv().compile(programSchema))
    });

    return {
      selectedItems,
      fakeRemove,
      tableCode,
      dateRange,
      ColorEnum,
      items,
      openDialog
    };
  },
});
</script>
