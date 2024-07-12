/**
 * plugins/index.ts
 *
 * Automatically included in `./src/main.ts`
 */

// Plugins
//import { loadFonts } from './webfontloader'
import { LanguagePlugin, TokenPlugin } from "@dative-gpi/foundation-extension-shared-ui";
import { PermissionPlugin, TranslationPlugin } from "@dative-gpi/bones-ui";

import vuetify from './vuetify'
import router from '../router'

// Types
import type { App } from 'vue'

export function registerPlugins(app: App) {
  // loadFonts()
  app
    .use(vuetify)
    .use(router)
    .use(PermissionPlugin)
    .use(TranslationPlugin)
    .use(LanguagePlugin)
    .use(TokenPlugin)
}
