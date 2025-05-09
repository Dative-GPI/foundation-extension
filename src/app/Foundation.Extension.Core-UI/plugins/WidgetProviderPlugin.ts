
import type { App, Plugin } from 'vue';
import { WIDGET_PROVIDER_KEY, WIDGET_CONFIGURATION_PROVIDER_KEY, type WidgetMap } from '../composables/useWidgetProvider';

export const WidgetProviderPlugin = (widgets: WidgetMap | null = null, widgetConfigurations: WidgetMap | null = null): Plugin => {
  return {
    install(app: App) {
      app.provide(WIDGET_PROVIDER_KEY, widgets);
      app.provide(WIDGET_CONFIGURATION_PROVIDER_KEY, widgetConfigurations);
    },
  };
};
