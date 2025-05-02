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

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes: [
    ...extensionRoutes,
    ...routes,
    ...dialogs
  ]
});

export default router;
