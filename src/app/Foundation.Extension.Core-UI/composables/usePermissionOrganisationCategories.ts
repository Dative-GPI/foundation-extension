import { ServiceFactory, ComposableFactory } from "@dative-gpi/bones-ui";

import { PermissionOrganisationCategoryInfos, type PermissionOrganisationCategoryInfosDTO } from "../domain";
import { PERMISSION_ORGANISATION_CATEGORIES_URL } from "../config";

const PermissionOrganisationCategoryServiceFactory = new ServiceFactory<PermissionOrganisationCategoryInfosDTO, PermissionOrganisationCategoryInfos>("extensionPermissionOrganisationCategory", PermissionOrganisationCategoryInfos).create(factory => factory.build(
  factory.addGetMany(PERMISSION_ORGANISATION_CATEGORIES_URL, PermissionOrganisationCategoryInfos),
  factory.addNotify()    
));

export const usePermissionOrganisationCategories = ComposableFactory.getMany(PermissionOrganisationCategoryServiceFactory);