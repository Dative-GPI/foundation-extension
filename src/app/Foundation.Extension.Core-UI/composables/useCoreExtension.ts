import { computed, ref, watch } from "vue";
import { useRouter } from "vue-router";

import { useExtensionHost, useTranslations } from "@dative-gpi/foundation-extension-shared-ui";
import { usePermissions as useAppPermissions, useTranslations as useAppTranslations } from "@dative-gpi/bones-ui";
import { Single } from "@dative-gpi/foundation-shared-domain/tools";

import { useAppOrganisationId } from "./useAppOrganisationId";
import { useCurrentPermissions } from "./useCurrentPermissions";
import { ORGANISATION_ID } from "../config/literals";
import { extractParams } from "../tools";

const single = new Single();

export const useCoreExtension = () => {
  return single.call(() => {
    const { initialized: organisationIdInitialized, setAppOrganisationId } = useAppOrganisationId();
    const { done: hostReady } = useExtensionHost();
    
    const { getMany: getCurrentPermission, entities: permissions } = useCurrentPermissions();
    const { set: setAppPermissions } = useAppPermissions();
    
    const { getMany: getManyTranslations, entities: translations} = useTranslations();
    const { set: setAppTranslations } = useAppTranslations();

    const router = useRouter();

    const ready = computed((): boolean => {
      return (hostReady.value && organisationIdInitialized.value);
    })

    const done = ref(false);

    watch(ready, async () => {
      if(!ready.value) {
        return;
      }
      await getCurrentPermission();
      setAppPermissions(permissions.value.map(p => p.toString()));
      
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
    });
    
    return {
      done
    };
  });
}