// Composables
import { RouteRecordRaw } from 'vue-router'
import drawersRoutes from "./drawers"


export const routes: RouteRecordRaw[] = [
    {
        path: "/admin/v1/application-translations",
        name: "application-translations",
        component: () => import('../views/Translations.vue'),
    },
    {  // TO REMOVE AFTER CHANGEfor admin -> application
        path: "/admin/v1/role-admins/:roleId",
        name: "permissions-application-role-admin",
        component: () => import('../views/RolePermissionApplications.vue'),
    },
    {
        path: "/admin/v1/role-applications/:roleId",
        name: "permissions-application-role-application",
        component: () => import('../views/RolePermissionApplications.vue'),
    },
    {
        path: "/admin/v1/organisation-types/:organisationTypeId/permissions",
        name: "permissions-organisation-type",
        component: () => import('../views/PermissionOrganisationTypes.vue'),
    },
    {
        path: "/admin/v1/role-organisation-types/:roleId",
        name: "role-permission-organisations",
        component: () => import('../views/RolePermissionOrganisations.vue'),
    },
    {
        path: "/admin/v1/organisation-types/:organisationTypeId/tables",
        name: "organisation-type-tables",
        component: () => import('../views/OrganisationTypeTables.vue'),
    },
    {
        path: "/admin/v1/organisation-types/:organisationTypeId/tables/:tableId",
        name: "organisation-type-table",
        component: () => import('../views/OrganisationTypeTable.vue'),
    },
    {
        path: "/admin/v1/tables",
        name: "tables",
        component: () => import('../views/Tables.vue'),
    },
    {
        path: "/admin/v1/tables/:tableId",
        name: "table",
        component: () => import('../views/Table.vue'),
    },
    {
        path: "/admin/v1/entities",
        name: "entities",
        component: () => import('../views/Entities.vue'),
    },
    ...drawersRoutes,
]