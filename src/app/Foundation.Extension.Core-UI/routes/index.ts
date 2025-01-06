import type { RouteRecordRaw } from "vue-router";

export const routes: RouteRecordRaw[] = [
  {
    path: "/organisations/:organisationId/admin-permission-organisations",
    name: "admin-permission-organisations",
    components: {
      default: () => import("../views/AdminPermissionOrganisations.vue")
    }
  },
  {
    path: "/organisations/:organisationId/role-permission-organisations/:roleId",
    name: "role-permission-organisations",
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
  },
  {
    path: "/organisations/:organisationId/dialogs/calendar-twin",
    name: "dialog-calendar-twin",
    components: {
      default: () => import("../views/dialogs/CalendarTwin.vue")
    }
  },
  {
    path: "/organisations/:organisationId/dialogs/role-permission-organisations/:roleId/update",
    name: "update-role-permission-organisation",
    components: {
      default: () => import("../views/dialogs/UpdateRolePermissionOrganisation.vue")
    }
  }
]