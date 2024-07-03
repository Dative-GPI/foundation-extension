import { ServiceFactory, ComposableFactory } from "@dative-gpi/bones-ui";

import { PERMISSIONS_URL, PERMISSION_CATEGORIES_URL } from "../config";

import type { PermissionCategoriesFilter, PermissionOrganisationCategoryDTO, PermissionOrganisationInfosDTO, PermissionsFilter } from "../domain";
import { PermissionOrganisationCategory, PermissionOrganisationInfos } from "../domain";

const PermissionServiceFactory = new ServiceFactory<PermissionOrganisationInfosDTO, PermissionOrganisationInfos>("permission-organisation", PermissionOrganisationInfos).create(factory => factory.build(
    factory.addGetMany<PermissionOrganisationInfosDTO, PermissionOrganisationInfos, PermissionsFilter>(PERMISSIONS_URL, PermissionOrganisationInfos),
    ServiceFactory.addCustom("getCategories",
        (axios, filter?: PermissionCategoriesFilter) => axios.get(PERMISSION_CATEGORIES_URL(), { params: filter }),
        (dtos: PermissionOrganisationCategoryDTO[]) => dtos.map(dto => new PermissionOrganisationCategory(dto))),
    factory.addNotify()    
));

export const usePermissionOrganisations = ComposableFactory.getMany(PermissionServiceFactory);
export const usePermissionOrganisationCategories = ComposableFactory.custom(PermissionServiceFactory.getCategories);
