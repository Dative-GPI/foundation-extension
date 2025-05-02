import type { RouteRecordRaw } from "vue-router";

export const routes: RouteRecordRaw[] = [
  // Iframes
  {
    path: "/organisations/:organisationId/role-organisations/:roleOrganisationId/permissions",
    name: "role-organisation-permissions",
    components: {
      default: () => import("../views/RoleOrganisationPermissions.vue")
    }
  },
  {
    path: "/organisations/:organisationId/role-organisation-types/:roleOrganisationTypeId/permissions",
    name: "role-organisation-type-permissions",
    components: {
      default: () => import("../views/RoleOrganisationTypePermissions.vue")
    }
  },
  {
    path: "/organisations/:organisationId/service-account-role-organisations/:serviceAccountRoleOrganisationId/permissions",
    name: "service-account-role-organisation-permissions",
    components: {
      default: () => import("../views/ServiceAccountRoleOrganisationPermissions.vue")
    }
  },
  {
    path: "/organisations/:organisationId/me/permissions",
    name: "me-permissions",
    components: {
      default: () => import("../views/MePermissions.vue")
    }
  },
  {
    path: "/organisations/:organisationId/user-organisations/:userOrganisationId/permissions",
    name: "user-organisation-permissions",
    components: {
      default: () => import("../views/UserOrganisationPermissions.vue")
    }
  },
  {
    path: "/organisations/:organisationId/service-account-organisations/:serviceAccountOrganisationId/permissions",
    name: "service-account-organisation-permissions",
    components: {
      default: () => import("../views/ServiceAccountOrganisationPermissions.vue")
    }
  },
  {
    path: "/organisations/:organisationId/widget-templates/:widgetTemplateId/widgets/:widgetId",
    name: "widget",
    components: {
      default: () => import("../views/Widget.vue")
    }
  },
  {
    path: "/organisations/:organisationId/widget-templates/:widgetTemplateId/configurations/:widgetId",
    name: "widget-configuration",
    components: {
      default: () => import("../views/WidgetConfiguration.vue")
    }
  },

  // Dialogs
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
    path: "/organisations/:organisationId/dialogs/role-organisations/:roleOrganisationId/update",
    name: "dialog-role-organisation-permissions",
    components: {
      default: () => import("../views/dialogs/UpdateRoleOrganisation.vue")
    }
  },
  {
    path: "/organisations/:organisationId/dialogs/service-account-role-organisations/:serviceAccountRoleOrganisationId/update",
    name: "dialog-service-account-role-organisation-permissions",
    components: {
      default: () => import("../views/dialogs/UpdateServiceAccountRoleOrganisation.vue")
    }
  }
];