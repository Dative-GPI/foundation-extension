<template>
  <FSCol
    :gap="16"
    v-if="table != null"
  >
    <v-data-table
      :item-class="() => 'cursor-pointer'"
      :items="columns"
      item-value="id"
      :headers="headers"
    >
      <template
        #item.dispoDisabled="{ item }"
      >
        <FSSwitch
          ref="element"
          :modelValue="!isDisabled(item.id)"
          @update:modelValue="setDisabled(item.id, !$event)"
          :disabled="!editMode"
          color="success"
        />
      </template>
      <template
        #item.dispoHidden="{ item }"
      >
        <FSSwitch
          ref="element"
          :modelValue="isDisabled(item.id) ? false : isHidden(item.id)"
          @update:modelValue="setHidden(item.id, $event)"
          :disabled="isDisabled(item.id) || !editMode"
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
  </FSCol>
</template>

<script lang="ts">
import { defineComponent, ref, computed, onMounted, watch } from "vue";

import _ from "lodash";

import { useTranslations } from "@dative-gpi/bones-ui/composables";

import type { Column, UpdateOrganisationTypeDispositionDTO, UpdateOrganisationTypeTableDTO } from "../../domain";
import { useOrganisationTypeTable, useTable, useUpdateOrganisationTypeTable } from "../../composables";
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
    organisationTypeId: {
      type: String,
      required: true,
    },
  },
  setup(props) {
    const { currentRoute } = useRouter();
    const { $tr } = useTranslations();

    const { fetch: getOrganisationTypeTable, entity: organisationTypeTable } = useOrganisationTypeTable();
    const { fetch: update } = useUpdateOrganisationTypeTable();
    const { get, entity: table } = useTable();

    const search = ref<string | undefined>();
    const columns = ref<Column[]>([]);
    const dispositions = ref<Omit<UpdateOrganisationTypeDispositionDTO, "index">[]>([]);

    const headers = computed(() => {
      return [
        {
          text: $tr("ui.common.label", "Name"),
          code: "ui.columns.label",
          title: "Label",
          value: "label",
        },
        {
          text: $tr("ui.organisation-type.disposition.data-access", "Data access"),
          code: "ui.columns.data-access",
          title: "Data access",
          value: "dispoDisabled",
        },
        {
          text: $tr("ui.organisation-type.disposition.display", "Cacher la colonne par défaut"),
          code: "ui.columns.display",
          title: "hidden",
          value: "dispoHidden",
        },
        ...(props.editMode
          ? [
            {
              text: $tr("ui.common.actions", "Actions"),
              value: "actions",
              title: "actions",
            },
          ]
          : []),
      ];
    });

    const init = async () => {
      await get(props.tableId);
      await getOrganisationTypeTable(props.organisationTypeId, props.tableId);
      reset();
    };

    const isHidden = (columnId: string) => {
      return dispositions.value.find((d) => d.columnId == columnId)?.hidden ?? false;
    };

    const setHidden = (columnId: string, hidden: boolean) => {
      const index = dispositions.value.findIndex((c) => c.columnId == columnId);
      if (index != -1) {
        dispositions.value[index].hidden = hidden;
      } else {
        dispositions.value.push({ columnId: columnId, disabled: false, hidden: hidden });
      }
    };

    const isDisabled = (columnId: string) => {
      return dispositions.value.find((d) => d.columnId == columnId)?.disabled ?? false;
    };

    const setDisabled = (columnId: string, disabled: boolean) => {
      const index = dispositions.value.findIndex((c) => c.columnId == columnId);
      if (index != -1) {
        dispositions.value[index].disabled = disabled;
      } else {
        dispositions.value.push({ columnId: columnId, disabled: disabled, hidden: false });
      }
      sortTable();
    };

    const up = (item: Column) => {
      let index = columns.value.indexOf(item);
      if (index > 0) {
        columns.value.splice(index, 1);
        columns.value.splice(index - 1, 0, item);
      }
      sortTable();
    };

    const down = (item: Column) => {
      let index = columns.value.indexOf(item);
      if (index < columns.value.length - 1) {
        columns.value.splice(index, 1);
        columns.value.splice(index + 1, 0, item);
      }
      sortTable();
    };

    const sortTable = () => {
      columns.value = columns.value
        .map((t, index) => ({ position: index, ...t }))
        .sort((a, b) => {
          if (isDisabled(a.id) == isDisabled(b.id)) {
            return a.position - b.position;
          }
          return +isDisabled(a.id) - +isDisabled(b.id);
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

      columns.value = table.value!.columns ? table.value!.columns.filter((c) => !c.disabled) : [];
      dispositions.value = organisationTypeTable.value?.dispositions ? organisationTypeTable.value?.dispositions : [];
      sortTable();
    };

    const save = async () => {
      if (table == null) {
        return;
      }

      const payload = columns.value
        .map((column, index) => {
          const disposition = dispositions.value.find((d) => d.columnId == column.id);
          if (disposition)
          {return {
            columnId: disposition.columnId,
            disabled: disposition.disabled,
            hidden: disposition.hidden,
            index: index,
          } as UpdateOrganisationTypeDispositionDTO;}
        })
        .filter((d) => !!d);

      await update(props.organisationTypeId, props.tableId, {
        dispositions: payload,
      } as UpdateOrganisationTypeTableDTO);
    };

    const onItemsDebounced = () => {
      if (props.editMode == true) {
        save();
      }
    };

    onMounted(init);

    const debouncedUpdateTable = _.debounce(onItemsDebounced, 500);

    watch(columns, debouncedUpdateTable, { deep: true });

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
      columns,
      up,
      down,
      sortTable,
      isDisabled,
      setDisabled,
      isHidden,
      setHidden,
    };
  },
});
</script>
