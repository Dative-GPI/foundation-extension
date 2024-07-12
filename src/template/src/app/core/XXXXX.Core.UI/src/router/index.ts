// Composables
import { createRouter, createWebHistory } from 'vue-router'

import { routes as templateRoutes } from "@dative-gpi/foundation-extension-core-ui"

const routes = [
  {
    path: '/organisations/:organisationId/XXXXX/examples',
    name: 'example',
    component: () => import('@/views/Home.vue'),
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes: [
    ...templateRoutes,
    ...routes
  ]
})

export default router
