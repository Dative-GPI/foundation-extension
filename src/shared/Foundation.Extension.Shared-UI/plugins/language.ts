import type { Plugin } from "vue";
import { ServiceFactory } from "@dative-gpi/bones-ui/core";
import { useAppLanguageCode } from "@dative-gpi/foundation-shared-services/composables";

const { languageCode } = useAppLanguageCode();

export const LanguagePlugin: Plugin = {
  install: () => {
    ServiceFactory.http.interceptors.request.use(config => {
      // Ajouter votre en-tête ici
      config.headers['X-Language-Code'] = `${languageCode.value}`;
      return config;
    }, error => {
      return Promise.reject(error);
    });
  }
}