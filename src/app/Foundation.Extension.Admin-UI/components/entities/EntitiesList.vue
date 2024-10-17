<template>
  <FSCol
    :gap="16"
  >
    <FSRow
      :gap="20"
    >
      <FSCol
        width="fill"
        style="max-width: 300px !important"
      >
        <FSTextField
          label=""
          prepend-inner-icon="mdi-magnify"
          @update:modelValue="getEntityProperties"
          v-model="search"
          width="fill"
          clearable
        ></FSTextField>
      </FSCol>

      <FSButton
        color="primary"
        variant="full"
        @click="openImport"
        prepend-icon="mdi-upload"
        :label="$tr('ui.common.import', 'Import')"
        class="align-self-end"
      />
      <FSButton
        :loading="downloading"
        prepend-icon="mdi-download"
        color="light"
        variant="full"
        @click="downloadEntityPropertyTranslation"
        :label="$tr('ui.common.export', 'Export')"
        class="align-self-end"
      />
      <FSButton
        v-if="selected.length == 1"
        color="primary"
        prepend-icon="mdi-pencil"
        @click="edit(selected[0])"
        :label="$tr('ui.common.update', 'Update')"
        class="align-self-end"
      />
    </FSRow>

    <FSRow>
      <v-data-table
        show-select
        v-model="selected"
        :items="filterEntities"
        :loading="fetching"
        :search="search"
        item-value="id"
        :headers="headers"
      >
        <template
          v-slot:item.entityType="{ item }"
        >
          <FSSpan>{{ extractEntityType(item.entityType) }}</FSSpan>
        </template>

        <template
          v-slot:item.languages="{ item }"
        >
          <v-chip-group>
            <FSChip
              v-for="(lang, index) in getLanguages(item)"
              :key="index"
              color="primary"
              :label="lang"
            > </FSChip>
          </v-chip-group>
        </template>
      </v-data-table>
    </FSRow>
  </FSCol>
</template>

<script lang="ts">
import { defineComponent, computed, onMounted, ref } from "vue";
import _ from "lodash";

import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui";
import { useTranslations } from "@dative-gpi/bones-ui/composables";

import type { EntityPropertyInfos } from "../../domain";
import {
  useApplicationLanguages,
  useEntityProtertyTranslations,
  useEntityProterties,
  useDownloadEntityPropertyTranslation,
} from "../../composables";
import { UPDATE_ENTITY_PROPERTY_DRAWER_URL } from "../../config";

export default defineComponent({
  name: "EntitiesList",
  components: {},
  setup() {
    const extension = useExtensionCommunicationBridge();
    const { $tr } = useTranslations();

    const {
      getMany: getEntityPropertyTranslations,
      entities: entityPropertyTranslations,
      fetching: fetchingEntityPropertyTranslations,
    } = useEntityProtertyTranslations();

    const {
      getMany: getEntityProperties,
      entities: entityProperties,
      fetching: fetchingEntityProperties,
    } = useEntityProterties();

    const {
      getMany: getApplicationLanguages,
      entities: languages,
      fetching: fetchingLanguages,
    } = useApplicationLanguages();

    const { fetch: download, entity: downloaded, fetching: downloading } = useDownloadEntityPropertyTranslation();

    const selected = ref<string[]>([]);
    const search = ref<string | undefined>();

    const headers = computed(() => {
      return [
        {
          title: $tr("ui.admin.entity-properties.code", "Code"),
          value: "code",
          sortable: true,
        },
        {
          title: $tr("ui.admin.entity-properties.label-default", "Default label"),
          value: "labelDefault",
        },
        {
          title: $tr("ui.admin.entity-properties.entity", "Entity"),
          value: "entityType",
          sortable: true,
          filterable: true,
          fixedFilters: _.uniqBy(
            entityProperties.value.map((p) => ({
              value: p.entityType,
              text: extractEntityType(p.entityType),
            })),
            "value"
          ),
        },
        {
          title: $tr("ui.admin.entity-properties.languages", "Languages"),
          value: "languages",
        },
      ];
    });

    const fetching = computed(() => {
      return fetchingLanguages.value || fetchingEntityProperties.value || fetchingEntityPropertyTranslations.value;
    });

    const filterEntities = computed(() => {
      return _.filter(entityProperties.value, (t) => {
        return (
          !t.parentId
        );
      });
    })

    const getLanguages = (item: EntityPropertyInfos) => {
      const ls = _(entityPropertyTranslations.value)
        .filter((at) => at.entityPropertyId == item.id)
        .map((t) => t.languageCode)
        .uniq()
        .value();

      return ls;
    };

    const downloadEntityPropertyTranslation = async () => {
      const fileName = "entityPropertytranslations.xlsx";
      await download({
        fileName: fileName,
      });

      const blob = new Blob([downloaded.value], {
        type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      });
      const link = document.createElement("a");
      link.href = URL.createObjectURL(blob);
      link.download = fileName;
      document.body.appendChild(link);
      link.click();
      link.remove();
    };

    const openImport = () => {
      extension.openDrawer("/admin/entitypropertytranslation/import-drawer");
    };

    const edit = async (entityPropertyId: string) => {
      const entitypropertySelected = entityProperties.value.find((t) => t.id == entityPropertyId);
      if (!entitypropertySelected) {return;}
      extension.openDrawer(UPDATE_ENTITY_PROPERTY_DRAWER_URL(entitypropertySelected?.id));
    };

    const extractEntityType = (entityType: string) => {
      const splitted = entityType.split(".");
      return `${splitted[1]}.${splitted[splitted.length - 1]}`;
    };

    const clear = () => {
      if (selected.value.length === 1) {
        entityPropertyTranslations.value = entityPropertyTranslations.value.filter(
          (at) => at.entityPropertyId !== selected.value[0]
        );
      }
    };

    const fetch = () => {
      getApplicationLanguages();
      getEntityProperties();
      getEntityPropertyTranslations();
    };

    onMounted(fetch);

    return {
      fetchingEntityPropertyTranslations,
      entityPropertyTranslations,
      entityProperties,
      fetching,
      downloading,
      filterEntities,
      search,
      selected,
      headers,
      downloadEntityPropertyTranslation,
      getEntityPropertyTranslations,
      openImport,
      getEntityProperties,
      extractEntityType,
      edit,
      getLanguages,
    };
  },
});
</script>

<style scoped></style>
