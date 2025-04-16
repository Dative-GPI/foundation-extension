// Composables
import { createRouter, createWebHistory } from 'vue-router';

import { routes as extensionRoutes } from "@dative-gpi/foundation-extension-core-ui";

const routes = [
  {
    path: '/organisations/:organisationId/XXXXX/examples',
    name: 'example',
    component: () => import('@/views/Home.vue'),
  },
];

const dialogs = [
  {
    path: '/organisations/:organisationId/XXXXX/examples/dialog',
    name: 'dialog',
    component: () => import('../views/DialogView.vue'),
  },
  {
    path: "/organisations/:organisationId/XXXXX/examples/action-dialog",
    name: "action-dialog",
    component: () => import('../views/dialogs/ActionDialog.vue'),
  }
];

const widgets = [
  {
    path: '/organisations/:organisationId/widget-templates/e2ba338e-e64a-427e-b886-0116a342cc15/widgets/:widgetId',
    name: 'widget',
    component: () => import('../views/widgets/TestWidget.vue'),
  }
];

const widgetConfigurations = [
  {
    path: '/organisations/:organisationId/widget-templates/e2ba338e-e64a-427e-b886-0116a342cc15/configurations/:widgetId',
    name: 'widget-configuration',
    component: () => import('../views/widgets/TestWidgetConfiguration.vue'),
  }
];

const router = createRouter({
  history: createWebHistory('/'),
  routes: [
    ...extensionRoutes,
    ...routes,
    ...dialogs,
    ...widgets,
    ...widgetConfigurations
  ]
});

export default router;
