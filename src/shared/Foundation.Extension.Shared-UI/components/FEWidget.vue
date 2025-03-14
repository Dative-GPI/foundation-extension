<template>
  <slot />
</template>

<script lang="ts">
import { computed, defineComponent, onMounted, onUnmounted, ref } from "vue";
import type { JTDSchemaType } from "ajv/dist/types/jtd-schema";
import { useRoute } from "vue-router";

import { useExtensionCommunicationBridge } from '@dative-gpi/foundation-extension-shared-ui';

export default defineComponent({
  name: "FEWidget",
  emits: ['update:meta', 'update:dashboardSettings', 'update:width', 'update:height'],
  setup(_props, { emit }) {
    const { subscribe, notify, unsubscribe } = useExtensionCommunicationBridge();
    const route = useRoute();

    const subcriberIds = ref<number[]>([]);
    const currentFullUrl = window.location.href;

    const widgetConfigurationSchema: JTDSchemaType<WidgetConfigurationPayload> = {
      properties: {
        meta: { type: "string" },
        dashboardSettings: { type: "string" },
        width: { type: "uint16" },
        height: { type: "uint16" }
      }
    };

    const widgetId = computed(() => {
      return route.params.widgetId;
    });

    const onReceiveNewConfig = (config: WidgetConfigurationPayload) => {
      emit('update:meta', JSON.parse(config.meta));
      emit('update:dashboardSettings', JSON.parse(config.dashboardSettings));
      emit('update:width', config.width);
      emit('update:height', config.height);
    }

    onMounted(() => {
      subcriberIds.value.push(
        subscribe(widgetConfigurationSchema, currentFullUrl, onReceiveNewConfig)
      )

      notify({
        mounted: true,
        widgetId: widgetId.value
      });
    });

    onUnmounted(() => {
      subcriberIds.value.forEach(subscriberId => {
        unsubscribe(subscriberId);
      });
    });

    return {
    };
  }
});

interface WidgetConfigurationPayload {
  meta: string,
  dashboardSettings: string,
  width: number,
  height: number
}
</script>