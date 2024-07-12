<template>
  <FSCol
    :gap="16"
  >
    <FSRow
      :gap="20"
    >
      <v-data-table
        :item-class="() => 'cursor-pointer'"
        @click:row="selectTable"
        :items="tables"
        :loading="fetching"
        :search="search"
        item-value="id"
        :headers="headers"
      >
      </v-data-table>
    </FSRow>
  </FSCol>
</template>

<script lang="ts">
import { defineComponent, ref, computed, onMounted } from "vue";

import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui";
import { useTables } from "../../composables";
import { useRouter } from "vue-router";

export default defineComponent({
  components: {},
  props: {
    editMode: {
      type: Boolean,
      required: true,
    },
  },
  setup() {
    const extension = useExtensionCommunicationBridge();
    const router = useRouter();

    const search = ref<string | undefined>();

    const { getMany, entities: tables, fetching } = useTables();

    const headers = computed(() => {
      return [
        {
          text: "Code",
          title: "Code",
          value: "code",
          sortable: true,
        },
        {
          text: "Name",
          title: "name",
          value: "label",
        },
      ];
    });

    const init = async () => {
      await getMany();
    };

    const selectTable = (click: Event, row: any) => {
      router.push({
        name: "organisation-type-table",
        params: { tableId: row.item.id },
      });
    };

    onMounted(init);

    return {
      headers,
      tables,
      fetching,
      search,
      selectTable,
    };
  },
});
</script>
