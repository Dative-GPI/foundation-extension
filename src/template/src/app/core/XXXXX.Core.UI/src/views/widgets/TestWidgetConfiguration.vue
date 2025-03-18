<template>
  <FEWidgetConfiguration
    v-model:widget="widget"
  >
    <FSCol
      v-if="widget?.meta"
      :gap="12"
    >
      <FSTextField
        label="Label"
        :modelValue="widget.meta.label ?? ''"
        @update:modelValue="onUpdateMetadaElement('label', $event)"
      />
    </FSCol>
  </FEWidgetConfiguration>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";

import type { Widget } from "@dative-gpi/foundation-core-domain/models";

import FEWidgetConfiguration from '@dative-gpi/foundation-extension-shared-ui/components/FEWidgetConfiguration.vue';

export default defineComponent({
  name: "TestWidgetConfiguration",
  components: {
    FEWidgetConfiguration
  },
  emits: ['update:widget'],
  setup() {
    const widget = ref<Widget | null>(null);

    const onUpdateWidget = (newWidget: any) => {
      widget.value = newWidget;
    }

    const onUpdateMetadaElement = (key: string, value: any) => {
      if(!widget.value) {
        return;
      }
      
      widget.value.meta[key] = value;
    }

    return {
      widget,
      onUpdateWidget,
      onUpdateMetadaElement
    };
  },
});
</script>
