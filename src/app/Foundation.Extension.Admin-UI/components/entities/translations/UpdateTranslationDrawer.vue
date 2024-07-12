<template>
  <Drawer :width="601"
    :title="$tr('ui.admin.translations.update-translation-drawer', 'Update entity property')"
    v-model:value="drawer">
    <v-skeleton-loader type="article"
      v-if="!entityProperty || !entityProtertyTranslations" />

    <template v-else>
      <FSCol :gap="12">
        <FSRow>
          <FSCol :items="items"
            class="mt-5">
            <FSSpan font="text-button"> Default label: {{ entityProperty.labelDefault }} </FSSpan><br />
            <FSSpan font="text-button"> Default category : {{ entityProperty.categoryLabelDefault }} </FSSpan>
          </FSCol>
        </FSRow>

        <FSRow>
          <FSCol v-if="fetchingLanguages">
            <v-skeleton-loader type="paragraph" />
          </FSCol>
          <FSCol width="fill"
            v-else>
            <FSCol v-for="l in languages"
              :key="l.code">
              <FSTextArea :rows="2"
                color="primary"
                :modelValue="getLabel(l.code)"
                @update:modelValue="setLabel(l.code, $event)"
                :key="l.code + '-' + l.code"
                :label="l.label"
                style="width: 97%" />
              <FSTextArea :rows="2"
                color="primary"
                :modelValue="getCategory(l.code)"
                @update:modelValue="setCategory(l.code, $event)"
                :key="l.code"
                :label="l.label"
                style="width: 97%" />
            </FSCol>
          </FSCol>
        </FSRow>
      </FSCol>
    </template>

    <template #actions>
      <v-spacer />
      <FSButton @click="close(true)"
        prepend-icon="mdi-cancel"
        :loading="updating"
        label="Cancel"> </FSButton>
      <FSButton class="ml-3 justify-content-end"
        @click="updateTranslations"
        :loading="updating"
        color="primary"
        prepend-icon="mdi-content-save-outline"
        label="Save"></FSButton>
    </template>
  </Drawer>
</template>

<script lang="ts">
import _ from "lodash";
import { defineComponent, ref, computed, onMounted } from "vue";

import { useRoute } from "vue-router";

import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui";

import {
  useEntityProterties,
  useUpdateEntityPropertyTranslation,
  useApplicationLanguages,
  useEntityProtertyTranslations,
} from "../../../composables";

import {
  EntityPropertyTranslationInfos,
  UpdateEntityPropertyTranslation,
} from "../../../domain";

import Drawer from "../../shared/Drawer.vue";

export default defineComponent({
  name: "UpdateTranslationDrawer",
  components: {
    Drawer,
  },
  setup(props, { emit }) {
    const route = useRoute();
    const extension = useExtensionCommunicationBridge();

    const {
      getMany: getEntityProperties,
      entities: entityProperties,
      fetching: fetchingEntityProperties,
    } = useEntityProterties();

    const drawer = ref<boolean>(true);

    const newTranslations = ref<UpdateEntityPropertyTranslation[]>([]);

    const { updated, update, updating } = useUpdateEntityPropertyTranslation();

    const {
      getMany: getEntityProtertyTranslations,
      entities: entityProtertyTranslations,
      fetching: fetchingEntityProtertyTranslations,
    } = useEntityProtertyTranslations();

    const { getMany: getManyLanguages, entities: languages, fetching: fetchingLanguages } = useApplicationLanguages();

    const items = [
      {
        key: "Code",
        code: "code",
      },
      {
        key: "Default translation",
        code: "default",
      },
    ];

    const entityPropId = route.params.entitypropertytranslationid.toString();

    const entityProperty = computed(() => {
      return entityProperties.value.find((x) => x.id == entityPropId);
    });

    const close = (success: boolean) => {
      extension.closeDrawer(location.pathname, success);
    };

    const setLabel = (languageCode: string, lab: string) => {
      const entityPropertyTranslation = newTranslations.value.find((x) => x.languageCode === languageCode);

      if (entityPropertyTranslation) { entityPropertyTranslation.label = lab; }
      else {
        newTranslations.value.push({
          languageCode: languageCode,
          label: lab,
          categoryLabel: ""
        } as UpdateEntityPropertyTranslation)
      }
    };

    const setCategory = (languageCode: string, categoryLab: string) => {
      const entityPropertyTranslation = newTranslations.value.find((x) => x.languageCode === languageCode);

      if (entityPropertyTranslation) { entityPropertyTranslation.categoryLabel = categoryLab; }
      else {
        newTranslations.value.push({
          languageCode: languageCode,
          label: "",
          categoryLabel: categoryLab,
        } as UpdateEntityPropertyTranslation)
      }
    };


    const updateTranslations = () => {

      newTranslations.value = newTranslations.value.filter((x) => x.label || x.categoryLabel);

      update(entityPropId, newTranslations.value).then(() => {
        close(true);
      });
    };

    const getLabel = (languageCode: string): string | undefined => {
      return newTranslations.value.find((x) => x.languageCode === languageCode)?.label;
    };

    const getCategory = (languageCode: string): string | undefined => {
      return newTranslations.value.find((x) => x.languageCode === languageCode)?.categoryLabel;
    };

    const fetch = async () => {
      getManyLanguages();
      getEntityProperties();
      await getEntityProtertyTranslations({ entityPropertyId: entityPropId });
      newTranslations.value = entityProtertyTranslations.value.map((x) => {
        return {
          languageCode: x.languageCode,
          label: x.label,
          categoryLabel: x.categoryLabel
        } as UpdateEntityPropertyTranslation;
      });
    };

    onMounted(fetch);

    return {
      drawer,
      updating,
      entityProperty,
      items,
      fetchingLanguages,
      languages,
      entityProtertyTranslations,
      updateTranslations,
      close,
      getLabel,
      getCategory,
      setLabel,
      setCategory
    };
  },
});
</script>

<style scoped></style>
