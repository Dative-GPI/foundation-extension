import { ServiceFactory, ComposableFactory } from "@dative-gpi/bones-ui";

import { PermissionOrganisationInfos, type PermissionOrganisationInfosDTO } from "../domain";
import { PERMISSION_ORGANISATIONS_URL } from "../config";

const PermissionOrganisationServiceFactory = new ServiceFactory<PermissionOrganisationInfosDTO,PermissionOrganisationInfos>("permissionOrganisation", PermissionOrganisationInfos).create(factory => factory.build(
  factory.addGetMany(PERMISSION_ORGANISATIONS_URL, PermissionOrganisationInfos),
  factory.addNotify()
));

export const usePermissionOrganisations = ComposableFactory.getMany(PermissionOrganisationServiceFactory);