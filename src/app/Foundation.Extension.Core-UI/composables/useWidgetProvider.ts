import { inject } from "vue";

export type WidgetMap = Record<string, any>;

export const WIDGET_PROVIDER_KEY = Symbol("extensionWidgetProvider");
export const WIDGET_CONFIGURATION_PROVIDER_KEY = Symbol("extensionWidgetConfigurationProvider");

export const useWidgetProvider = () => {
  const widgets = inject(WIDGET_PROVIDER_KEY) as WidgetMap | null;
  const widgetConfigurations = inject(WIDGET_CONFIGURATION_PROVIDER_KEY) as WidgetMap | null;

  const getWidget = (templateId: string) => {
    return widgets?.[templateId] || null;
  };

  const getWidgetConfiguration = (templateId: string) => {
    return widgetConfigurations?.[templateId] || null;
  }

  return { getWidget, getWidgetConfiguration };
};
