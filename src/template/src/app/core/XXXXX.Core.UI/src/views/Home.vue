<template>
  <FEEntityView
    title="Example"
    subtitle="Sous titre"
    :breadcrumbs="breadcrumbs"
  >
    <template
      #description
    >
      <FSChip
        variant="full"
        :color="ColorEnum.Light"
      >
        I'm a chip from #description slot
      </FSChip>
    </template>
    <template
      #toolbar
    >
      <FSButton
        icon="mdi-pen"
        label="I'm a toolbar button"
        :color="ColorEnum.Primary"
      />
    </template>
    <ExampleComponent />
  </FEEntityView>
</template>

<script lang="ts">
import { defineComponent, computed } from "vue";

import { type FSBreadcrumbItem, ColorEnum } from "@dative-gpi/foundation-shared-components/models";
import { useTranslations } from "@dative-gpi/bones-ui";

import FEEntityView from "@dative-gpi/foundation-extension-shared-ui/components/FEEntityView.vue"

import ExampleComponent from "@/components/ExampleComponent.vue";

export default defineComponent({
  name: "Home",
  components: {
    ExampleComponent,
    FEEntityView
  },
  setup() {
    const { $tr } = useTranslations();

    const breadcrumbs = computed((): FSBreadcrumbItem[] => {
      return [
        {
          title: $tr('page.example', 'Example'),
          disabled: true
        },
        {
          title: $tr('page.example', 'Breadcrumbs'),
          disabled: true
        }
      ]
    });

    return {
      ColorEnum,
      breadcrumbs
    };
  },
});
</script>

<style scoped></style>
