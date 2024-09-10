import type { RouteRecordRaw } from "vue-router";

export const routes: RouteRecordRaw[] = [
    {
        path: "/organisations/:organisationId/role-organisations/:roleId",
        name: "role-organisation",
        components: {
            default: () => import("../views/RolePermissionOrganisations.vue")
        }
    },
    {
      path: "/organisations/:organisationId/dialogs/remove",
      name: "dialog-remove",
      components: {
          default: () => import("../views/dialogs/Remove.vue")
      }
    }
]