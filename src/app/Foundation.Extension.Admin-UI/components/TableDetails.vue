<template>
  <FSCol
    :gap="16"
    v-if="table != null"
  >
    <v-data-table
      :item-class="() => 'cursor-pointer'"
      :items="items"
      item-value="id"
      :headers="headers"
    >
      <template
        #item.disabled="{ item }"
      >
        <FSSwitch
          ref="element"
          :modelValue="!item.disabled"
          @update:modelValue="
            item.disabled = !$event;
            sortTable();
          "
          :editable="editMode"
          color="success"
        />
      </template>
      <template
        #item.hidden="{ item }"
      >
        <FSSwitch
          v-if="!item.disabled"
          ref="element"
          :modelValue="item.hidden"
          @update:modelValue="item.hidden = $event"
          :editable="editMode"
          color="success"
        />
        <FSSwitch
          v-else
          :disabled="true"
          ref="element"
          :modelValue="false"
          :editable="editMode"
          color="success"
        />
      </template>

      <template
        #item.sortable="{ item }"
      >
        <FSSwitch
          ref="element"
          :modelValue="item.sortable"
          @update:modelValue="item.sortable = $event"
          :editable="editMode"
          color="success"
        />
      </template>

      <template
        #item.filterable="{ item }"
      >
        <FSSwitch
          ref="element"
          :modelValue="item.filterable"
          @update:modelValue="item.filterable = $event"
          :editable="editMode"
          color="success"
        />
      </template>

      <template
        #item.configurable="{ item }"
      >
        <FSSwitch
          ref="element"
          :modelValue="item.configurable"
          @update:modelValue="item.configurable = $event"
          :editable="editMode"
          color="success"
        />
      </template>
      <template
        #item.actions="{ item }"
      >
        <FSButton
          v-if="editMode"
          @click="up(item)"
          variant="icon"
          icon="mdi-arrow-up"
        />
        <FSButton
          v-if="editMode"
          @click="down(item)"
          variant="icon"
          icon="mdi-arrow-down"
        />
      </template>
    </v-data-table>
    <table-synchronizer
      :edit-mode="editMode"
      :table-id="tableId"
    />
  </FSCol>
</template>

<script lang="ts">
import { defineComponent, ref, computed, onMounted, watch } from "vue";

import _ from "lodash";

import TableSynchronizer from "./TableSynchronizer.vue";

import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui";
import { useTable, useUpdateTable } from "../composables";
import { Column } from "../domain";
import { useRouter } from "vue-router";

export default defineComponent({
  components: {
    TableSynchronizer,
  },
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
  setup(props) {
    const { setTitle, setCrumbs } = useExtensionCommunicationBridge();
    const { currentRoute } = useRouter();
    const { get, entity: table, getting } = useTable();
    const { update } = useUpdateTable();

    const search = ref<string | undefined>();
    const items = ref<Column[]>([]);

    const headers = computed(() => {
      return [
        {
          text: $tr("ui.name", "Name"),
          title: "label",
          value: "label",
        },
        {
          text: "Access",
          title: "Access",
          value: "disabled",
        },
        {
          text: "Cacher a colonne par défaut",
          title: "hidden",
          value: "hidden",
        },
        {
          text: "Triable",
          title: "sortable",
          value: "sortable",
        },
        {
          text: "Filterable",
          title: "filterable",
          value: "filterable",
        },
        {
          text: "Configurable",
          title: "configurable",
          value: "configurable",
        },
        ...(props.editMode
          ? [
            {
              text: "Actions",
              title: "Actions",
              value: "actions",
            },
          ]
          : []),
      ];
    });

    const init = async () => {
      setCrumbs([
        {
          to: currentRoute.value.path,
          text: "Table",
          disabled: true,
        },
      ]);
      await get(props.tableId);
      reset();
      setTitle($tr("ui.xxxxx.table", "Table"));
    };

    const up = (item: Column) => {
      let index = items.value.indexOf(item);
      items.value.splice(index, 1);
      items.value.splice(index - 1, 0, item);

      sortTable();
    };

    const down = (item: Column) => {
      let index = items.value.indexOf(item);
      items.value.splice(index, 1);
      items.value.splice(index + 1, 0, item);

      sortTable();
    };

    const sortTable = () => {
      items.value = items.value
        .map((t, index) => ({ position: index, ...t }))
        .sort((a, b) => {
          if (a.disabled == b.disabled) {
            return a.position - b.position;
          }
          return +a.disabled - +b.disabled;
        })
        .map((t) => {
          let { position, ...others } = t;
          return others;
        });
    };

    const reset = () => {
      if (table == null) {
        return;
      }

      items.value = table.value?.columns ? table.value?.columns.map((c) => new Column(c)) : [];
      sortTable();
    };

    const save = async () => {
      if (table == null) {
        return;
      }

      const payload = items.value.map((c, i) => {
        let { index, ...others } = c;
        return {
          index: i,
          ...others,
        };
      });
      await update(table.value!.id, { columns: payload });
    };

    const onItemsDebounced = () => {
      if (props.editMode == true) {
        save();
      }
    };

    onMounted(init);

    const debouncedUpdateTable = _.debounce(onItemsDebounced, 500);

    watch(items, debouncedUpdateTable, { deep: true });

    watch(
      () => props.editMode,
      () => {
        if (props.editMode == false) {
          save();
        }
      }
    );

    watch(
      () => table,
      () => {
        reset();
      }
    );

    return {
      headers,
      table,
      search,
      items,
      up,
      down,
      sortTable,
    };
  },
});
</script>
