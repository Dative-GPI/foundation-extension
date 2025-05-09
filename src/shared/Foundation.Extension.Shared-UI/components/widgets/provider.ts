const widgets: { [key: string]: any } = {}
const configurations: { [key: string]: any } = {}

export const registerWidgets = (newWidgets: Record<string, any>, newWidgetConfigurations: Record<string, any>) => {
  for (const key in newWidgets) {
    widgets[key] = newWidgets[key]
  }
  for (const key in newWidgetConfigurations) {
    configurations[key] = newWidgetConfigurations[key]
  }
}

export const useWidgetProvider = () => {
  const get = (widgetTemplateId: keyof typeof widgets) => {
    const widget = widgets[widgetTemplateId];
    if (!widget) {
      return null;
    }
    return widget;
  };

  return {
    get
  };
};

export const useConfigurationProvider = () => {
  const get = (code: keyof typeof configurations) => {
    const configuration = configurations[code];
    if (!configuration) {
      return null;
    }
    return configuration;
  };

  return {
    get
  };
};