/**
 * plugins/index.ts
 *
 * Automatically included in `./src/main.ts`
 */

// Plugins
import { loadFonts } from './webfontloader'
import vuetify from './vuetify'
import { LanguagePlugin } from './language';
import router from '../router'

import { PermissionPlugin, TranslationPlugin } from "@dative-gpi/bones-ui";

// Types
import type { App } from 'vue'
import { OrganisationPlugin } from './organisation';
import { TokenPlugin } from './token';

/* const permissionOptions: PermissionOptions = {
  permissionsProvider: usePermissionsProvider()
}

const translationOptions: TranslationOptions = {
  translationsProvider: useTranslationsProvider()
} */

export function registerPlugins(app: App) {
  loadFonts()
  app
    .use(vuetify)
    .use(router)
    .use(PermissionPlugin)
    .use(TranslationPlugin)
    .use(LanguagePlugin)
    .use(TokenPlugin)
    .use(OrganisationPlugin)
}
