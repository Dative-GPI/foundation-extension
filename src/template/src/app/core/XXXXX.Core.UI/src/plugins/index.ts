/**
 * plugins/index.ts
 *
 * Automatically included in `./src/main.ts`
 */

// Plugins
import { registerWidgets } from "@dative-gpi/foundation-extension-shared-ui/components/widgets/provider";
import { LanguagePlugin, TokenPlugin } from "@dative-gpi/foundation-extension-shared-ui";
import { PermissionPlugin, TranslationPlugin } from "@dative-gpi/bones-ui";
import { OrganisationPlugin } from './organisation';
import { loadFonts } from './webfontloader'
import vuetify from './vuetify'
import router from '../router'

import { widgetConfigurations, widgets } from '../router/widgets';

// Types
import type { App } from 'vue'
export function registerPlugins(app: App) {
  loadFonts();
  registerWidgets(widgets, widgetConfigurations);
  app
    .use(vuetify)
    .use(router)
    .use(PermissionPlugin)
    .use(TranslationPlugin)
    .use(LanguagePlugin)
    .use(TokenPlugin)
    .use(OrganisationPlugin)
}
