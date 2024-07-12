import { ServiceFactory } from "@dative-gpi/bones-ui";
import { ComposableFactory } from "@dative-gpi/bones-ui";

import { CURRENT_USER_PERMISSIONS_URL } from "../config";

const CurrentPermissionServiceFactory = new ServiceFactory("current-permissions", String)
    .create(f => f.build(
        f.addGetMany(CURRENT_USER_PERMISSIONS_URL, String),
        f.addNotify()
    ));

export const useCurrentPermissions = ComposableFactory.getMany(CurrentPermissionServiceFactory);
