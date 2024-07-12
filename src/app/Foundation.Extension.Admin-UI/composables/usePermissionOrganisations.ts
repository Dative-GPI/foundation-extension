import { ServiceFactory, ComposableFactory } from "@dative-gpi/bones-ui";

import { PERMISSION_ORGANISATION_CATEGORIES_URL, PERMISSION_ORGANISATIONS_URL } from "../config";

import type { PermissionCategoriesFilter, PermissionOrganisationCategoryDTO, PermissionOrganisationInfosDTO } from "../domain";
import { PermissionOrganisationCategory, PermissionOrganisationInfos } from "../domain";

const PermissionOrganisationServiceFactory = new ServiceFactory<PermissionOrganisationInfosDTO,PermissionOrganisationInfos>("permission-organisation", PermissionOrganisationInfos)
    .create(f => f.build(
        f.addGetMany(PERMISSION_ORGANISATIONS_URL, PermissionOrganisationInfos),
        ServiceFactory.addCustom("getCategories",
            (axios, filter? : PermissionCategoriesFilter) => axios.get(PERMISSION_ORGANISATION_CATEGORIES_URL(), { params: filter }),
            (dtos: PermissionOrganisationCategoryDTO[]) => dtos.map(dto => new PermissionOrganisationCategory(dto))),
        f.addNotify()
    )
);

export const usePermissionOrganisations = ComposableFactory.getMany(PermissionOrganisationServiceFactory);
export const usePermissionOrganisationCategories = ComposableFactory.custom(PermissionOrganisationServiceFactory.getCategories);