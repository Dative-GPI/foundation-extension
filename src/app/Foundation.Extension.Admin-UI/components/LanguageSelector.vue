<template>
  <FSSelectField
    :items="languages"
    :disabled="disabled"
    :label="trueLabel"
    :clearable="!disabled"
    item-value="code"
    :required="true"
    item-title="label"
    v-model="selectValue"
    :item-props="languageProps"
    style="max-width: 350px;"
  >
  </FSSelectField>
</template>

<script lang="ts">
import { defineComponent, computed, onMounted, ref } from "vue";

import { useApplicationLanguages } from "../composables";
import type { Language } from "../domain";

export default defineComponent({
  name: "LanguageSelector",
  props: {
    disabled: {
      type: Boolean,
      required: false,
      default: false,
    },
    label: {
      type: String,
      required: false,
      default: null,
    },
  },
  setup(props) {
    const { getMany, entities: languages } = useApplicationLanguages();

    const selectValue = ref<string | null>(null);

    const trueLabel = computed(() => {
      return props.label ?? "Language";
    });

    const fetch = () => {
      getMany();
    };

    const languageProps = (item: Language) => {
      return {
        title: item.label,
        icon: item.icon,
        code: item.code,
        id: item.id,
      };
    };

    onMounted(fetch);

    return {
      languages,
      trueLabel,
      languageProps,
      selectValue
    };
  },
});
</script>

<style scoped></style>
