/**
 * plugins/index.ts
 *
 * Automatically included in `./src/main.ts`
 */

// Plugins
import { LanguagePlugin, TokenPlugin } from "@dative-gpi/foundation-extension-shared-ui";
import { WidgetProviderPlugin } from "@dative-gpi/foundation-extension-core-ui";
import { PermissionPlugin, TranslationPlugin } from "@dative-gpi/bones-ui";
import { OrganisationPlugin } from './organisation';
import { loadFonts } from './webfontloader'
import vuetify from './vuetify'
import router from '../router'

import { widgetConfigurations, widgets } from '../router/widgets';

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
    .use(WidgetProviderPlugin(widgets, widgetConfigurations))
}
