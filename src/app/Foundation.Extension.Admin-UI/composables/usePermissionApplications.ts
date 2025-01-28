import { ServiceFactory, ComposableFactory } from "@dative-gpi/bones-ui";

import { PERMISSION_APPLICATIONS_URL, PERMISSION_APPLICATIONS_CURRENT_URL } from "../config";
import { PermissionApplicationInfos, type PermissionApplicationInfosDTO} from "../domain";

const PermissionApplicationServiceFactory = new ServiceFactory<PermissionApplicationInfosDTO, PermissionApplicationInfos>("permissionApplication", PermissionApplicationInfos).create(factory => factory.build(
  factory.addGetMany(PERMISSION_APPLICATIONS_URL, PermissionApplicationInfos),
  ServiceFactory.addCustom("getCurrent", (axios) => axios.get(PERMISSION_APPLICATIONS_CURRENT_URL), (dtos: string[]) => dtos),
  factory.addNotify()    
));

export const usePermissionApplications = ComposableFactory.getMany(PermissionApplicationServiceFactory);
export const useCurrentPermissionApplications = ComposableFactory.custom(PermissionApplicationServiceFactory.getCurrent);