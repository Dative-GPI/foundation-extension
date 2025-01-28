import { computed, ref, watch } from "vue";
import { useRouter } from "vue-router";

import { usePermissions as useAppPermissions, useTranslations as useAppTranslations } from "@dative-gpi/bones-ui";
import { useExtensionHost, useTranslations } from "@dative-gpi/foundation-extension-shared-ui";
import { useAppOrganisationId } from "@dative-gpi/foundation-core-services/composables";
import { Single } from "@dative-gpi/foundation-shared-domain/tools";

import { useCurrentPermissionOrganisations } from "./usePermissionOrganisations";
import { useCurrentUserOrganisation } from "./useUserOrganisations";
import { ORGANISATION_ID } from "../config/literals";
import { extractParams } from "../tools";

const single = new Single();

export const useCoreExtension = () => {
  return single.call(() => {
    const { fetch: getCurrentPermissionOrganisations, entity: currentPermissionOrganisations } = useCurrentPermissionOrganisations();
    const { fetch: getCurrentUserOrganisation, entity: currentUserOrganisation } = useCurrentUserOrganisation();
    const { setAppOrganisationId, ready: organisationIdInitialized } = useAppOrganisationId();
    const { getMany: getManyTranslations, entities: translations} = useTranslations();
    const { set: setAppTranslations } = useAppTranslations();
    const { set: setAppPermissions } = useAppPermissions();
    const { done: hostReady } = useExtensionHost();

    const router = useRouter();

    const ready = computed((): boolean => {
      return (hostReady.value && organisationIdInitialized.value);
    });

    const done = ref(false);

    watch(ready, async () => {
      if (!ready.value) {
        return;
      }

      await getCurrentUserOrganisation();

      await getCurrentPermissionOrganisations();
      setAppPermissions(currentPermissionOrganisations.value);
      
      await getManyTranslations();
      setAppTranslations(translations.value);

      done.value = true;
    });

    watch(router.currentRoute, () => {
      const params = extractParams(window.location.pathname, `/organisations/:${ORGANISATION_ID}`);
      const routeOrganisationId = params ? params[ORGANISATION_ID] : null;
      if (routeOrganisationId) {
        setAppOrganisationId(routeOrganisationId);
      }
    }, { immediate: true });
    
    return {
      currentUserOrganisation,
      done
    };
  });
}