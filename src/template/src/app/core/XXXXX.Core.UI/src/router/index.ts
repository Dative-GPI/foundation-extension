// Composables
import { createRouter, createWebHistory } from 'vue-router'

import { routes as extensionRoutes } from "@dative-gpi/foundation-extension-core-ui"

const routes = [
  {
    path: '/organisations/:organisationId/XXXXX/examples',
    name: 'example',
    component: () => import('@/views/Home.vue'),
  },
]

const drawers = [
  {
    path: '/organisations/:organisationId/XXXXX/examples/drawer',
    name: 'drawer',
    component: () => import('@/views/DialogView.vue'),
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes: [
    ...extensionRoutes,
    ...routes,
    ...drawers
  ]
})

export default router
