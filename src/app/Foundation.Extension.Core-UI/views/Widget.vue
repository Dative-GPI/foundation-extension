<template>
  <FSLoader
    v-if="waitingForConfig"
    width="100%"
    height="100%"
  />
  <template
    v-else
  >
    <component
      :is="widgetComponent"
      v-model:meta="meta"
      :settings="dashboardSettings"
      :widgetWidth="width"
      :widgetHeight="height"
    />
  </template>
</template>

<script lang="ts">
import { computed, defineComponent, onMounted, onUnmounted, ref } from "vue";
import type { JTDSchemaType } from "ajv/dist/types/jtd-schema";

import { useRoute } from "vue-router";
import { useExtensionCommunicationBridge } from '@dative-gpi/foundation-extension-shared-ui';
import { useWidgetProvider } from '@dative-gpi/foundation-extension-shared-ui/components/widgets/provider';

import type { DashboardSettings } from "@dative-gpi/foundation-core-domain/models";

export default defineComponent({
  name: "WidgetView",
  setup() {
    const { subscribe, notify, unsubscribe } = useExtensionCommunicationBridge();
    const { get: getWidget } = useWidgetProvider();
    const route = useRoute();

    const meta = ref<object | null>(null);
    const width = ref<number | null>(null);
    const height = ref<number | null>(null);
    const dashboardSettings = ref<DashboardSettings | null>(null);
    const waitingForConfig = ref(true);
    const subcriberIds = ref<number[]>([]);
    const currentFullUrl = window.location.href;

    const widgetComponent = computed(() => {
      const widgetTemplateId = route.params.widgetTemplateId as string;
      return getWidget(widgetTemplateId);
    });

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
      meta.value = JSON.parse(config.meta);
      dashboardSettings.value = JSON.parse(config.dashboardSettings);
      width.value = config.width;
      height.value = config.height;

      waitingForConfig.value = false;
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
      meta,
      width,
      height,
      widgetComponent,
      waitingForConfig,
      dashboardSettings
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