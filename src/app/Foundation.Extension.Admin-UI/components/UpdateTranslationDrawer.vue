<template>
  <Drawer
    :width="601"
    :title="$tr('ui.admin.translations.update-translation-drawer', 'Update translation')"
    v-model:value="drawer"
  >
    <v-skeleton-loader
      type="article"
      v-if="!translation || fetchingApplicationTranslations"
    />

    <template
      v-else
    >
      <FSCol
        :gap="12"
      >
        <FSRow>
          <FSCol
            :items="items"
            class="mt-5"
          >
            <FSSpan
              font="text-button"
            > {{ translation.code }} : {{ translation.value }} </FSSpan>
          </FSCol>
        </FSRow>

        <FSRow>
          <FSCol
            v-if="fetchingLanguages"
          >
            <v-skeleton-loader
              type="paragraph"
            />
          </FSCol>
          <FSCol
            width="fill"
            v-else
          >
            <FSTextArea
              v-for="l in languages"
              :modelValue="getValue(l.code)"
              @update:modelValue="setValue(l.code, $event)"
              :key="translation.code + '-' + l.code"
              :label="l.label"
              style="width: 97%"
            />
          </FSCol>
        </FSRow>
      </FSCol>

    </template>

    <template
      #actions
    >
      <v-spacer />
      <FSButton
        @click="close(true)"
        :loading="upserting"
        label="Cancel"
        prepend-icon="mdi-cancel"
        color="light"
      > </FSButton>
      <FSButton
        class="ml-3 justify-content-end"
        @click="updateTranslations"
        color="primary"
        :loading="upserting"
        prepend-icon="mdi-content-save-outline"
        label="Save"
      ></FSButton>
    </template>
  </Drawer>
</template>

<script lang="ts">
import { defineComponent, ref, computed, onMounted } from "vue";

import { useRoute } from "vue-router";

import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui";

import { useApplicationTranslations, useUpsertApplicationTranslation, useApplicationLanguages } from "../composables";

import type { Translation, UpdateApplicationTranslationLanguage } from "../domain";
import { ApplicationTranslation } from "../domain";

import Drawer from "./shared/Drawer.vue";

export default defineComponent({
  name: "UpdateTranslationDrawer",
  components: {
    Drawer,
  },
  setup() {
    const route = useRoute();
    const extension = useExtensionCommunicationBridge();

    const drawer = ref<boolean>(true);

    const newTranslations = ref<UpdateApplicationTranslationLanguage[]>([]);

    const { fetch: upsert, fetching: upserting } = useUpsertApplicationTranslation();

    const {
      getMany: getManyApplicationTranslations,
      entities: applicationTranslations,
      fetching: fetchingApplicationTranslations,
    } = useApplicationTranslations();

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

    const params = route.params.translation.toString().split(";") as string[];

    const translation = computed((): Translation => {
      return { id: params[0], code: params[1], value: atob(params[2]) };
    });

    const close = (success: boolean) => {
      extension.closeDrawer(location.pathname, success);
    };

    const setValue = (languageCode: string, ev: string) => {
      const applicationTranslation = newTranslations.value.find((x) => x.languageCode === languageCode && x.translationCode == translation.value.code);
      if (applicationTranslation) {
        if (!ev || ev.length == 0) {
          newTranslations.value = newTranslations.value.filter((x) => x.languageCode !== languageCode && x.translationCode == translation.value.code);
        } else {
          applicationTranslation.value = ev;
        }
      } else {
        if (ev && ev.length > 0) {
          pushTranslation(languageCode, ev);
        }
      }
    };

    const pushTranslation = (languageCode: string, ev: string) => {
      newTranslations.value.push({
        languageCode: languageCode,
        translationCode: translation.value.code,
        value: ev,
      } as UpdateApplicationTranslationLanguage);
    };

    const updateTranslations = () => {
      upsert(translation.value.code, {
        translations: newTranslations.value,
      }).then(() => {
        close(true);
      });
    };

    const getValue = (languageCode: string): string | null | undefined => {
      return newTranslations.value.find((x) => x.languageCode === languageCode && x.translationCode == translation.value.code)?.value;
    };

    const fetch = async () => {
      await getManyLanguages();
      await getManyApplicationTranslations({ translationCode: translation.value.code });
      newTranslations.value = applicationTranslations.value.map((at) => new ApplicationTranslation(at));
    };

    onMounted(fetch);

    return {
      items,
      drawer,
      languages,
      upserting,
      translation,
      fetchingLanguages,
      applicationTranslations,
      fetchingApplicationTranslations,
      updateTranslations,
      getValue,
      setValue,
      close
    };
  },
});
</script>

<style scoped></style>
