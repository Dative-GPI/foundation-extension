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
        <FSSearchField
          prepend-inner-icon="mdi-magnify"
          v-model="search"
          width="fill"
          clearable
        ></FSSearchField>
      </FSCol>

      <FSButton
        v-if="$props.editMode"
        color="primary"
        variant="full"
        @click="openImport"
        prepend-icon="mdi-upload"
        :label="$tr('ui.common.import', 'Import')"
        class="align-self-end"
      />
      <FSButton
        v-if="$props.editMode"
        :loading="downloading"
        prepend-icon="mdi-download"
        color="light"
        variant="full"
        @click="downloadTranslation"
        :label="$tr('ui.common.export', 'Export')"
        class="align-self-end"
      />
      <FSButton
        v-if="selected.length == 1 && $props.editMode"
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
        :items="translations"
        :loading="fetching"
        :search="search"
        item-value="id"
        :headers="headers"
      >
        <template
          v-slot:item.languages="{ item }"
        >
          <v-chip-group>
            <FSChip
              v-for="(lang, index) in getLanguages(item)"
              :key="index"
              style="margin-left: 5px;"
            > {{ lang }}</FSChip>
          </v-chip-group>
        </template>
      </v-data-table>
    </FSRow>
  </FSCol>
</template>

<script lang="ts">
import { defineComponent, ref, computed, onMounted } from "vue";

import _ from "lodash";

import { IMPORT_TRANSLATIONS_DRAWER_URL, UPDATE_TRANSLATION_DRAWER_URL } from "../config";

import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui";

import type { Translation } from "../domain/models";

import {
  useTranslations,
  useApplicationTranslations,
  useApplicationLanguages,
  useDownloadApplicationTranslation,
} from "../composables";

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

    const search = ref<string | undefined>();

    const { getMany: getTranslations, entities: translations, fetching: fetchingTranslations } = useTranslations();
    const {
      getMany: getApplicationTranslations,
      entities: applicationTranslations,
      fetching: fetchingApplicationTranslations,
    } = useApplicationTranslations();

    const { fetch: download, entity: downloaded, fetching: downloading } = useDownloadApplicationTranslation();

    const {
      getMany: getApplicationLanguages,
      entities: languages,
      fetching: fetchingLanguages,
    } = useApplicationLanguages();

    const fetching = computed(() => {
      return fetchingLanguages.value || fetchingTranslations.value || fetchingApplicationTranslations.value;
    });

    const selected = ref<string[]>([]);

    const headers = computed(() => {
      return [
        {
          title: "Code",
          value: "code",
          sortable: true,
        },
        {
          title: "Default translation",
          value: "value",
        },
        {
          title: "Translations",
          value: "languages",
        },
      ];
    });

    const getLanguages = (item: Translation) => {
      const ls = _(applicationTranslations.value)
        .filter((at) => at.translationCode == item.code)
        .map((t) => t.languageCode)
        .uniq()
        .value();

      return ls;
    };

    const openImport = () => {
      extension.openDrawer(IMPORT_TRANSLATIONS_DRAWER_URL);
    };

    const edit = async (translationId: string) => {
      const translationSelected = translations.value.find((t) => t.id == translationId);
      if (!translationSelected) {return;}
      const path = `${translationSelected?.id};${translationSelected?.code};${btoa(translationSelected!.value)}`;
      extension.openDrawer(UPDATE_TRANSLATION_DRAWER_URL(path));
    };

    const downloadTranslation = async () => {
      const fileName = "translations.xlsx";
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

    const init = () => {
      getApplicationLanguages();
      getTranslations({ prefix: search.value });
      getApplicationTranslations();
    };

    onMounted(init);

    return {
      headers,
      translations,
      fetching,
      selected,
      languages,
      search,
      downloading,
      edit,
      getLanguages,
      getTranslations,
      openImport,
      downloadTranslation,
    };
  },
});
</script>
