import type { Plugin } from "vue";
import { ServiceFactory } from "@dative-gpi/bones-ui/core";
import { useAppOrganisationId } from "@dative-gpi/foundation-core-services/composables";

const { organisationId } = useAppOrganisationId();

export const OrganisationPlugin: Plugin = {
  install: () => {
    ServiceFactory.http.interceptors.request.use(config => {
      // Ajouter votre en-tête ici
      config.headers['X-Organisation-Id'] = `${organisationId.value}`;
      return config;
    }, error => {
      return Promise.reject(error);
    });
  }
}