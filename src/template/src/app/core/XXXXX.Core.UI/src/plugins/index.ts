/**
 * plugins/index.ts
 *
 * Automatically included in `./src/main.ts`
 */

// Plugins
import { PermissionPlugin, TranslationPlugin } from "@dative-gpi/bones-ui";
import { LanguagePlugin, TokenPlugin } from "@dative-gpi/foundation-extension-shared-ui";
import { loadFonts } from './webfontloader'
import vuetify from './vuetify'
import router from '../router'

import { OrganisationPlugin } from './organisation';

// Types
import type { App } from 'vue'
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
