<template>
  <FEDialog
    :title="$tr('ui.dashboard.widget-configuration', 'Widget configuration')"
    class="fe-dialog-content"
    width="900px"
    v-model="dialog"
  >
    <template
      #body
    >
      <FSDialogFormBody
        v-if="widgetTemplate"
        @click:submitButton="onSubmit"
        @click:cancelButton="dialog = false"
      >
        <template
          #body
        > 
          <FSCol
            gap="24px"
          >
            <FSCard
              height="100px"
              width="100%"
            >
              <FSCol
                align="center-center"
              >
                <FSIcon
                  v-if="widgetTemplate.icon"
                  size="32px"
                >
                  {{ widgetTemplate.icon }}
                </FSIcon>
                <FSText
                  font="text-overline"
                >
                  {{ widgetTemplate.label }}
                </FSText>
              </FSCol>
            </FSCard>
            <FSRow
              v-if="showStandardOptions"
              align="bottom-left"
              :wrap="false"
            >
              <FSNumberField
                :label="$tr('entity.widget.width', 'Width')"
                v-model="width"
              />
              <FSNumberField
                :label="$tr('entity.widget.height', 'Height')"
                v-model="height"
              />
              <FSSwitch
                class="dialog-configure-widget-hide-borders"
                :label="$tr('entity.widget.hide-borders', 'Hide borders')"
                v-model="hideBorders"
              />
            </FSRow>
            <component
              :is="widgetConfigurationComponent"
              v-model:meta="meta"
              v-model:settings="dashboardSettings"
            />
          </FSCol>
        </template>
        <template
          v-if="showReset"
          #left-footer
        >
          <FSButton
            :label="$tr('ui.dashboard.configure-shallow-widget-reset', 'Reset configuration')"
            color="primary"
            @click="resetWidget"
          />
        </template>
      </FSDialogFormBody>
    </template>
  </FEDialog>
</template>

<script lang="ts">
import { computed, defineComponent, onUnmounted, ref, watch } from "vue";
import type { JTDSchemaType } from "ajv/dist/types/jtd-schema";
import { useRoute } from 'vue-router';

import { useExtensionCommunicationBridge } from '@dative-gpi/foundation-extension-shared-ui';
import { useConfigurationProvider } from '@dative-gpi/foundation-extension-shared-ui/components/widgets/provider';

import FEDialog from '@dative-gpi/foundation-extension-shared-ui/components/FEDialog.vue';
import type { DashboardSettings, Widget, WidgetTemplateInfos } from "@dative-gpi/foundation-core-domain/models";

export default defineComponent({
  name: "WidgetConfiguration",
  components: {
    FEDialog
  },
  setup() {
    const { subscribe, notify, unsubscribe } = useExtensionCommunicationBridge();
    const { get: getWidgetConfiguration } = useConfigurationProvider();
    const route = useRoute();

    const width = ref(0);
    const height = ref(0);
    const dialog = ref(true);
    const showReset = ref(false);
    const hideBorders = ref(false);
    const meta = ref<object | null>(null);
    const showStandardOptions = ref(false);
    const subcriberIds = ref<number[]>([]);
    const widget = ref<Widget | null>(null);
    const widgetTemplate = ref<WidgetTemplateInfos | null>(null);
    const dashboardSettings = ref<DashboardSettings | null>(null);
    
    const widgetConfigurationSchema: JTDSchemaType<WidgetInfosPayload> = {
      properties: {
        widget: { type: "string" },
        widgetTemplate: { type: "string" },
        dashboardSettings: { type: "string" },
        showStandardOptions: { type: "boolean" },
        showReset: { type: "boolean" }
      }
    };

    const widgetConfigurationComponent = computed(() => {
      const widgetTemplateId = route.params.widgetTemplateId as string;
      return getWidgetConfiguration(widgetTemplateId);
    });

    const currentFullUrl = computed(() => window.location.href);

    const onReceiveNewConfig = (config: WidgetInfosPayload) => {
      widget.value = JSON.parse(config.widget);

      widgetTemplate.value = JSON.parse(config.widgetTemplate);
      showStandardOptions.value = config.showStandardOptions;
      showReset.value = config.showReset;

      if(widget.value) {
        meta.value = widget.value.meta;
        width.value = widget.value.width;
        height.value = widget.value.height;
        hideBorders.value = widget.value.hideBorders;
      }
    }

    const onSubmit = () => {
      const widgetUpdate: WidgetUpdate = {
        widget: JSON.stringify({
          ...widget.value,
          meta: meta.value,
          width: width.value,
          height: height.value,
          hideBorders: hideBorders.value
        })
      };

      notify(widgetUpdate);
      dialog.value = false;
    }

    const resetWidget = () => {
      const widgetUpdate: WidgetUpdate = {
        widget: JSON.stringify({
          ...widget.value,
          meta: null
        })
      };

      notify(widgetUpdate);
      dialog.value = false;
    }

    watch(() => currentFullUrl.value, () => {
      subcriberIds.value.forEach(subscriberId => {
        unsubscribe(subscriberId);
      });

      subcriberIds.value.push(
        subscribe(widgetConfigurationSchema, currentFullUrl.value, onReceiveNewConfig)
      )

      const mountedPayload: MountedPayload = {
        mounted: true
      };
      notify(mountedPayload);
    }, { immediate: true });

    onUnmounted(() => {
      subcriberIds.value.forEach(subscriberId => {
        unsubscribe(subscriberId);
      });
    });

    return {
      meta,
      width,
      dialog,
      height,
      widget,
      showReset,
      hideBorders,
      widgetTemplate,
      dashboardSettings,
      showStandardOptions,
      widgetConfigurationComponent,
      onSubmit,
      resetWidget
    };
  }
});

interface MountedPayload {
  mounted: boolean
}

interface WidgetInfosPayload {
  widget: string,
  widgetTemplate: string,
  dashboardSettings: string,
  showStandardOptions: boolean,
  showReset: boolean
}

interface WidgetUpdate {
  widget: string
}
</script>