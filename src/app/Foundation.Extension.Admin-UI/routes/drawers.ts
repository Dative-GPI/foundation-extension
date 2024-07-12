import { RouteRecordRaw } from "vue-router";
import { IMPORT_TRANSLATIONS_DRAWER_URL } from "../config";

const drawersRoutes: RouteRecordRaw[] = [
    {
        path: "/admin/translations/:translation/drawer",
        name: "update-translation-drawer",
        components: {
            default: () => import("../components/UpdateTranslationDrawer.vue"),
        },
        meta: {
            exact: false,
            drawer: true
        }
    },
    {
        path: IMPORT_TRANSLATIONS_DRAWER_URL,
        name: "import-translations-drawer",
        components: {
            default: () => import("../components/ImportTranslationsDrawer.vue"),
        },
        meta: {
            exact: false,
            drawer: true
        }
    },
    {
        path: `/admin/entitypropertytranslation/import-drawer`,
        name: "import-entity-property-translations-drawer",
        components: {
            default: () => import("../components/entities/translations/ImportTranslationsDrawer.vue"),
        },
        meta: {
            exact: false,
            drawer: true
        }
    },
    {
        path: "/admin/entitypropertytranslation/:entitypropertytranslationid/drawer",
        name: "update-entity-property-translation-drawer",
        components: {
            default: () => import("../components/entities/translations/UpdateTranslationDrawer.vue"),
        },
        meta: {
            exact: false,
            drawer: true
        }
    },
]

export default drawersRoutes;