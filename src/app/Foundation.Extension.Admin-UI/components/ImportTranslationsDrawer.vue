<template>
  <Drawer
    class="pa-1"
    :width="601"
    v-bind="$attrs"
    :title="$tr('ui.admin.translations.import-drawer', 'Import translations')"
    v-model:value="drawer"
  >
    <FSCol
      :gap="12"
      class="pa-2"
      align="center-center"
    >
      <FSDivider
        :label="$tr('ui.admin.translations.workbook', 'Upload workbook')"
      />

      <v-form
        v-model="valid"
        ref="form"
        lazy-validation
      >
        <FSCol
          :gap="12"
        >
          <FSSpan
            font="text-body text-wrap"
          >
            {{
              $tr(
                "ui.admin.translations.workbook-description",
                "Please select a workbook from your device. The first column must include the translations codes"
              )
            }}
          </FSSpan>
          <FSRow
            class="align-center"
          >
            <FSButtonFile
              class="mr-2"
              prepend-icon="mdi-upload"
              accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"
              :readFile="false"
              color="primary"
              @update:modelValue="onUpload"
              :label="$tr('ui.admin.translations.workbook', 'Workbook')"
            />

            <FSRow
              v-if="workbook != null"
            >
              <FSSpan
                font="text-h3 mr-2"
              >
                {{ workbook.name }}
              </FSSpan>
              <FSButton
                class="mr-2"
                variant="icon"
                color="error"
                icon="mdi-minus-circle-outline"
                @click="workbook = null"
              />
            </FSRow>
          </FSRow>
          <FSRow
            align="center-center"
          >
            <FSDivider
              :label="$tr('ui.admin.translations.languages', 'Languages')"
            />
          </FSRow>
          <FSRow
            class="align-center mb-5"
          >
            <FSSpan
              font="text-body text-wrap"
            >
              {{
                $tr("ui.admin.translations.add-a-column", "Click the plus button for each column containing a language")
              }}
            </FSSpan>
            <v-spacer />
            <FSButton
              variant="icon"
              icon="mdi-plus-circle-outline"
              @click="() => columns.push({ index: nextIndex(), languageCode: '' })"
            />
          </FSRow>
          <FSCol
            v-if="columns.length > 0"
          >
            <FSRow
              v-for="(column, i) in columns"
              :key="i"
              align="bottom-center"
            >
              <FSRow
                align="center-center"
              >
                <FSButton
                  class="mr-2"
                  variant="icon"
                  color="error"
                  icon="mdi-minus-circle-outline"
                  @click="() => columns.splice(i, 1)"
                />
                <FSText
                  font="text-h2"
                >
                  {{ $tr("ui.admin.translations.column", "Column") }}
                  {{ indexToColumn(column.index) }}
                </FSText>
                <v-spacer />
                <FSButton
                  class="mr-2"
                  variant="icon"
                  icon="mdi-menu-left-outline"
                  :disabled="column.index < 2"
                  @click="() => column.index--"
                />
                <FSButton
                  class="mr-2"
                  variant="icon"
                  icon="mdi-menu-right-outline"
                  @click="() => column.index++"
                />
              </FSRow>
              <LanguageSelector
                :rules="[(v: string) => !!v || $tr('ui.common.required', 'Required')]"
                style="width: 280px"
                @update:model-value="setLanguageCode(i, $event)"
              />
            </FSRow>
          </FSCol>
        </FSCol>
      </v-form>
    </FSCol>

    <template
      #actions
    >
      <v-spacer />
      <FSButton
        @click="closeDrawer"
        class="mr-2"
        :label="$tr('ui.common.cancel', 'Cancel')"
        prepend-icon="mdi-cancel"
      > </FSButton>
      <FSButton
        prepend-icon="mdi-upload"
        :loading="uploading"
        variant="standard"
        @click="upload"
        color="primary"
        :label="$tr('ui.common.import', 'Import')"
      />
    </template>
  </Drawer>
</template>

<script lang="ts">
import type { ApplicationTranslationsColumn } from "../domain";
import { defineComponent, ref } from "vue";

import Drawer from "./shared/Drawer.vue";
import LanguageSelector from "./LanguageSelector.vue";

import { useUploadApplicationTranslation } from "../composables";
import _ from "lodash";
import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui";

export default defineComponent({
  name: "ImportTranslationsDrawer",
  components: {
    LanguageSelector,
    Drawer,
  },

  setup() {
    const drawer = ref<boolean>(true);
    const valid = ref<boolean>(false);

    const extension = useExtensionCommunicationBridge();

    const {
      fetch: uploadApplicationTranslations,
      entity: applicationTranslations,
      fetching: uploading,
    } = useUploadApplicationTranslation();

    const form = ref<any>();

    const workbook = ref<File | null>(null);
    const columns = ref<ApplicationTranslationsColumn[]>([]);

    const onUpload = (payload: File) => {
      workbook.value = payload;
    };

    const closeDrawer = () => {
      extension.closeDrawer(location.pathname, false);
    };

    const indexToColumn = (index: number) => {
      const letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      let column = "";

      while (index >= 0) {
        column = letters[index % 26] + column;
        index = Math.floor(index / 26) - 1;
      }
      return column;
    };

    const nextIndex = () => {
      if (columns.value.length === 0) {
        return 2;
      }
      return _.maxBy(columns.value, "index")!.index + 1;
    };

    const upload = async () => {
      if (!form.value.validate() || workbook.value == null) {
        return;
      }

      await uploadApplicationTranslations({
        file: workbook.value,
        languagesCodes: columns.value,
      }).then(() => {
        extension.notify(applicationTranslations.value);
        extension.closeDrawer(location.pathname, true);
      });
    };

    const setLanguageCode = (index: number, event: string) => {
      columns.value[index].languageCode = event;
    };

    return {
      form,
      valid,
      drawer,
      columns,
      workbook,
      uploading,
      setLanguageCode,
      indexToColumn,
      closeDrawer,
      nextIndex,
      onUpload,
      upload,
    };
  },
});
</script>

<style scoped></style>
