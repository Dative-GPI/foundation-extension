import { ServiceFactory, ComposableFactory } from "@dative-gpi/bones-ui";

import { PERMISSION_ORGANISATIONS_CURRENT_URL, PERMISSION_ORGANISATIONS_URL } from "../config";
import { PermissionOrganisationInfos, type PermissionOrganisationInfosDTO } from "../domain";

const PermissionServiceFactory = new ServiceFactory<PermissionOrganisationInfosDTO, PermissionOrganisationInfos>("extensionPermissionOrganisation", PermissionOrganisationInfos).create(factory => factory.build(
  factory.addGetMany(PERMISSION_ORGANISATIONS_URL, PermissionOrganisationInfos),
  ServiceFactory.addCustom("getCurrent", (axios) => axios.get(PERMISSION_ORGANISATIONS_CURRENT_URL()), (dtos: string[]) => dtos),
  factory.addNotify()
));

export const usePermissionOrganisations = ComposableFactory.getMany(PermissionServiceFactory);
export const useCurrentPermissionOrganisations = ComposableFactory.custom(PermissionServiceFactory.getCurrent);