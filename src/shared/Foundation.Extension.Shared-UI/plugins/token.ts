import type { Plugin } from "vue";
import { ServiceFactory } from "@dative-gpi/bones-ui/core";
import { useAppAuthToken } from "@dative-gpi/foundation-shared-services/composables";

const { authToken } = useAppAuthToken();

export const TokenPlugin: Plugin = {
  install: () => {
    ServiceFactory.http.interceptors.request.use(config => {
      // Ajouter votre en-tête ici
      config.headers.Authorization = `Bearer ${authToken.value}`;
      return config;
    }, error => {
      return Promise.reject(error);
    });
  }
}