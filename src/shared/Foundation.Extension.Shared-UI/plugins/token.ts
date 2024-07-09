import type { Plugin } from "vue";
import { ServiceFactory } from "@dative-gpi/bones-ui/core";
import { useAppToken } from "@dative-gpi/foundation-extension-shared-ui";

const { token } = useAppToken();

export const TokenPlugin: Plugin = {
  install: () => {
    ServiceFactory.http.interceptors.request.use(config => {
      // Ajouter votre en-tête ici
      config.headers.Authorization = `Bearer ${token.value}`;
      return config;
    }, error => {
      return Promise.reject(error);
    });
  }
}