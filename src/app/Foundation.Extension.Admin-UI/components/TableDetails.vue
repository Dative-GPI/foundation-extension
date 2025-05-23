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
          :disabled="!editMode"
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
          :disabled="!editMode"
          color="success"
        />
        <FSSwitch
          v-else
          :disabled="true"
          ref="element"
          :modelValue="false"
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
          :disabled="!editMode"
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
          :disabled="!editMode"
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
          :disabled="!editMode"
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
    <TableSynchronizer
      :edit-mode="editMode"
      :table-id="tableId"
    />
  </FSCol>
</template>

<script lang="ts">
import { defineComponent, ref, computed, watch } from "vue";
import { useRouter } from "vue-router";

import _ from "lodash";

import TableSynchronizer from "./TableSynchronizer.vue";

import { useTranslations } from "@dative-gpi/bones-ui/composables";

import { useTable, useUpdateTable } from "../composables";
import { Column } from "../domain";

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
    const { get, entity: table } = useTable();
    const { currentRoute } = useRouter();
    const { update } = useUpdateTable();
    const { $tr } = useTranslations();

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

    const debouncedUpdateTable = _.debounce(onItemsDebounced, 500);

    watch(items, debouncedUpdateTable, { deep: true });

    watch(
      () => props.tableId, () => {
        if(props.tableId) {
          get(props.tableId);
        }
      }, { immediate: true });

    watch(
      () => props.editMode,
      () => {
        if (props.editMode == false) {
          save();
        }
      }
    );

    watch(
      () => table.value,
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
