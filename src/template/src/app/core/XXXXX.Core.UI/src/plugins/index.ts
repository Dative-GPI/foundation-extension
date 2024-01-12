/**
 * plugins/index.ts
 *
 * Automatically included in `./src/main.ts`
 */

// Plugins
import { loadFonts } from './webfontloader'
import vuetify from './vuetify'
import router from '../router'

import { PermissionPlugin, PermissionOptions, TranslationPlugin, TranslationOptions } from "@dative-gpi/bones-ui"
import { usePermissionsProvider } from '@dative-gpi/foundation-template-core-ui'
import { useTranslationsProvider } from '@dative-gpi/foundation-template-shared-ui'

// Types
import type { App } from 'vue'

const permissionOptions: PermissionOptions = {
  permissionProvider: usePermissionsProvider()
}

const translationOptions: TranslationOptions = {
  translationProvider: useTranslationsProvider()
}

export function registerPlugins(app: App) {
  loadFonts()
  app
    .use(vuetify)
    .use(router)
    .use(PermissionPlugin, permissionOptions)
    .use(TranslationPlugin, translationOptions)
}
