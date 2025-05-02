import { defineAsyncComponent } from 'vue';

export const widgets = {
  "e2ba338e-e64a-427e-b886-0116a342cc15": defineAsyncComponent(() => import('../views/widgets/TestWidget.vue'))
}

export const widgetConfigurations = {
  "e2ba338e-e64a-427e-b886-0116a342cc15": defineAsyncComponent(() => import('../views/widgets/TestWidgetConfiguration.vue'))
}