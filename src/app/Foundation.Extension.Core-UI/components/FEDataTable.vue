<template>
  <FSLoadDataTable
    v-if="!initialized || gettingUserOrganisationTable"
  />
  <FSDataTableUI
    v-else
    @update:rowsPerPage="table.rowsPerPage = $event"
    @update:filters="table.filters = $event"
    @update:headers="table.headers = $event"
    @update:sortBy="table.sortBy = $event"
    @update:mode="table.mode = $event"
    @update:page="table.page = $event"
    v-bind="{ ...computedTable, ...$attrs }"
  >
    <template
      v-for="(_, name) in $slots"
      v-slot:[name]="slotData"
    >
      <slot
        :name="name"
        v-bind="slotData"
      />
    </template>
  </FSDataTableUI>
</template>

<script lang="ts">
import { computed, defineComponent, onUnmounted, type PropType, watch } from "vue";

import { useDebounce, useTables } from "@dative-gpi/foundation-shared-components/composables";
import type { FSDataTableColumn } from "@dative-gpi/foundation-shared-components/models";
import { useDataTables } from "@dative-gpi/foundation-core-services/composables";

import { useUpdateUserOrganisationTable, useUserOrganisationTable } from "../composables";

import FSLoadDataTable from "@dative-gpi/foundation-shared-components/components/lists/FSLoadDataTable.vue";
import FSDataTableUI from "@dative-gpi/foundation-shared-components/components/lists/FSDataTableUI.vue";

export default defineComponent({
  name: "FEDataTable",
  components: {
    FSLoadDataTable,
    FSDataTableUI
  },
  props: {
    defaultMode: {
      type: String as PropType<"table" | "iterator">,
      required: false,
      default: "table"
    },
    tableCode: {
      type: String as PropType<string | null>,
      required: false,
      default: null
    },
    debounceTime: {
      type: Number,
      required: false,
      default: 1000
    },
    headersOptions: {
      type: Object as PropType<{ [key: string]: Partial<FSDataTableColumn> }>,
      required: false,
      default: () => ({})
    }
  },
  setup(props) {
    const { get: getUserOrganisationTable, getting: gettingUserOrganisationTable } = useUserOrganisationTable();
    const { initialized, table, updateTable, computeTable, onTableCodeChange } = useDataTables();
    const { update: updateUserOrganisationTable } = useUpdateUserOrganisationTable();
    const { getTable, setTable } = useTables();
    const { debounce, cancel } = useDebounce();

    const computedTable = computed(() => computeTable(props.headersOptions, props.defaultMode));

    onUnmounted(() => {
      cancel();
      update();
    });

    const update = () => {
      if (props.tableCode) {
        updateTable(updateUserOrganisationTable, setTable, props.tableCode, props.defaultMode);
      }
    }

    watch(() => props.tableCode, async (): Promise<void> => {
      onTableCodeChange(getUserOrganisationTable, getTable, props.tableCode, props.defaultMode);
    }, { immediate: true });

    watch(() => (table.value ? { ...table.value } : null), (_, former) => {
      if (table.value && former && initialized.value) {
        debounce(update, props.debounceTime);
      }
    }, { deep: true });

    return {
      gettingUserOrganisationTable,
      computedTable,
      initialized,
      table
    };
  },
});
</script>