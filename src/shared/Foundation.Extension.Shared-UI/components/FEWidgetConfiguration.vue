<template>
  <FEDialog
    :title="$tr('ui.dashboard.widget-configuration', 'Widget configuration')"
    class="fe-dialog-content"
    height="fit-content"
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
            <slot />
          </FSCol>
        </template>
      </FSDialogFormBody>
    </template>
  </FEDialog>
</template>

<script lang="ts">
import { computed, defineComponent, onUnmounted, ref, watch } from "vue";
import type { JTDSchemaType } from "ajv/dist/types/jtd-schema";

import { useExtensionCommunicationBridge } from '@dative-gpi/foundation-extension-shared-ui';
import FEDialog from '@dative-gpi/foundation-extension-shared-ui/components/FEDialog.vue';

export default defineComponent({
  name: "FEWidgetConfiguration",
  components: {
    FEDialog
  },
  props: {
    widget: {
      type: Object,
      required: false
    }
  },
  emits: ['update:widget', 'update:widgetTemplate', 'update:dashboardSettings'],
  setup(props, { emit }) {
    const { subscribe, notify, unsubscribe } = useExtensionCommunicationBridge();

    const dialog = ref(true);
    const subcriberIds = ref<number[]>([]);
    const widget = ref<object | null>(null);
    const widgetTemplate = ref<object | null>(null);
    const dashboardSettings = ref<object | null>(null);
    
    const width = ref(0);
    const height = ref(0);
    const hideBorders = ref(false);
    
    const widgetConfigurationSchema: JTDSchemaType<WidgetInfosPayload> = {
      properties: {
        widget: { type: "string" },
        widgetTemplate: { type: "string" },
        dashboardSettings: { type: "string" }
      }
    };

    const currentFullUrl = computed(() => window.location.href);

    const onReceiveNewConfig = (config: WidgetInfosPayload) => {
      const widget = JSON.parse(config.widget);
      const dashboardSettings = JSON.parse(config.dashboardSettings);

      widgetTemplate.value = JSON.parse(config.widgetTemplate);

      emit('update:widget', widget);
      emit('update:dashboardSettings', dashboardSettings);
      emit('update:widgetTemplate', widgetTemplate.value);

      if(widget.value) {
        width.value = widget.width;
        height.value = widget.height;
        hideBorders.value = widget.hideBorders;
      }
    }

    const onSubmit = () => {
      const widgetUpdate: WidgetUpdate = {
        widget: JSON.stringify(widget.value)
      };

      notify(widgetUpdate);
      dialog.value = false;
    }

    watch(() => props.widget, (newWidget) => {
      widget.value = newWidget;

      if(newWidget) {
        width.value = newWidget.width;
        height.value = newWidget.height;
        hideBorders.value = newWidget.hideBorders;
      }
    }, { deep: true });

    watch([width, height, hideBorders], () => {
      const newWidget = {
        ...widget.value,
        width: width.value,
        height: height.value,
        hideBorders: hideBorders.value
      };

      emit('update:widget', newWidget);
    });

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
      width,
      dialog,
      height,
      widget,
      hideBorders,
      widgetTemplate,
      dashboardSettings,
      onSubmit
    };
  }
});

interface MountedPayload {
  mounted: boolean
}

interface WidgetInfosPayload {
  widget: string,
  widgetTemplate: string,
  dashboardSettings: string
}

interface WidgetUpdate {
  widget: string
}
</script>