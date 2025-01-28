import { ServiceFactory, ComposableFactory } from "@dative-gpi/bones-ui";

import { PERMISSION_ORGANISATION_CATEGORIES_URL } from "../config";

import { PermissionOrganisationCategoryInfos, type PermissionOrganisationCategoryInfosDTO } from "../domain";

const PermissionOrganisationCategoryServiceFactory = new ServiceFactory<PermissionOrganisationCategoryInfosDTO, PermissionOrganisationCategoryInfos>("permissionOrganisationCategory", PermissionOrganisationCategoryInfos).create(factory => factory.build(
  factory.addGetMany(PERMISSION_ORGANISATION_CATEGORIES_URL, PermissionOrganisationCategoryInfos),
  factory.addNotify()    
));

export const usePermissionOrganisationCategories = ComposableFactory.getMany(PermissionOrganisationCategoryServiceFactory);