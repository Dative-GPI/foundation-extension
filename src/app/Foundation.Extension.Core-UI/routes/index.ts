import type { RouteRecordRaw } from "vue-router";

export const routes: RouteRecordRaw[] = [
  {
    path: "/organisations/:organisationId/service-account-role-organisations/:roleId",
    name: "service-account-role-organisation",
    components: {
      default: () => import("../views/ServiceAccountRoleOrganisationPermissionOrganisations.vue")
    }
  },
  {
    path: "/organisations/:organisationId/role-organisation-types/:roleId",
    name: "role-organisation-type",
    components: {
      default: () => import("../views/RoleOrganisationTypePermissionOrganisations.vue")
    }
  },
  {
    path: "/organisations/:organisationId/role-organisations/:roleId",
    name: "role-organisation",
    components: {
      default: () => import("../views/RoleOrganisationPermissionOrganisations.vue")
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
    path: "/organisations/:organisationId/dialogs/service-account-role-organisations/:roleId/update",
    name: "update-service-account-role-organisation",
    components: {
      default: () => import("../views/dialogs/UpdateServiceAccountRoleOrganisation.vue")
    }
  },
  {
    path: "/organisations/:organisationId/dialogs/role-organisations/:roleId/update",
    name: "update-role-organisation",
    components: {
      default: () => import("../views/dialogs/UpdateRoleOrganisation.vue")
    }
  }
]