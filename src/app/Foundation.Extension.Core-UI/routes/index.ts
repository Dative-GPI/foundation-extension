import type { RouteRecordRaw } from "vue-router";

export const routes: RouteRecordRaw[] = [
    {
        path: "/organisations/:organisationId/role-organisations/:roleId",
        name: "role-organisation",
        components: {
            default: () => import("../views/RolePermissionOrganisations.vue")
        }
    },
]