import { onMounted, ref } from "vue";
import { useExtensionHost, useTranslations } from "@dative-gpi/foundation-extension-shared-ui";
import { usePermissions as useAppPermissions, useTranslations as useAppTranslations } from "@dative-gpi/bones-ui";
import { Single } from "@dative-gpi/foundation-shared-domain/tools";

import { useCurrentPermissions } from "./useCurrentPermissions";

const single = new Single();

export const useAdminExtension = () => {
    return single.call(() => {
      const { getMany: getCurrentPermission, entities: permissions } = useCurrentPermissions();
      const { set: setAppPermissions } = useAppPermissions();
      
      const { getMany: getManyTranslations, entities: translations} = useTranslations();
      const { set: setAppTranslations } = useAppTranslations();
  
      const done = ref(false);
      
      onMounted(async () => {       
        useExtensionHost();
         
        await getCurrentPermission();
        setAppPermissions(permissions.value.map(p => p.toString()));
        
        await getManyTranslations();
        setAppTranslations(translations.value);
  
        done.value = true;
      });
  
      return {
        permissions,
        done
      };
    });
  }
