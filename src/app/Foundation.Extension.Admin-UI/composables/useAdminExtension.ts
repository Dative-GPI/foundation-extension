import { onMounted, ref } from "vue";

import { usePermissions as useAppPermissions, useTranslations as useAppTranslations } from "@dative-gpi/bones-ui";
import { useExtensionHost, useTranslations } from "@dative-gpi/foundation-extension-shared-ui";
import { Single } from "@dative-gpi/foundation-shared-domain/tools";

import { useCurrentPermissionApplications } from "./usePermissionApplications";

const single = new Single();

export const useAdminExtension = () => {
  return single.call(() => {
    const { fetch: getCurrentPermissionApplications, entity: currentPermissionApplications } = useCurrentPermissionApplications();
    const { set: setAppPermissions } = useAppPermissions();
    
    const { getMany: getManyTranslations, entities: translations} = useTranslations();
    const { set: setAppTranslations } = useAppTranslations();

    const done = ref(false);
    
    onMounted(async () => {
      useExtensionHost();
      
      await getCurrentPermissionApplications();
      if (currentPermissionApplications.value != null) {
        setAppPermissions(currentPermissionApplications.value);
      }
      
      await getManyTranslations();
      setAppTranslations(translations.value);

      done.value = true;
    });

    return {
      currentPermissionApplications,
      done
    };
  });
}
