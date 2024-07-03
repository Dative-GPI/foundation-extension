import { ServiceFactory, ComposableFactory } from "@dative-gpi/bones-ui";

import { PERMISSION_APPLICATION_CATEGORIES_URL, PERMISSION_APPLICATIONS_URL } from "../config";

import type { PermissionApplicationCategoriesFilter, PermissionApplicationCategoryDTO, PermissionApplicationFilter, PermissionApplicationInfosDTO} from "../domain";
import { PermissionApplicationInfos, PermissionApplicationCategory} from "../domain";

const PermissionApplicationServiceFactory = new ServiceFactory<PermissionApplicationInfosDTO, PermissionApplicationInfos>("permission-application", PermissionApplicationInfos).create(factory => factory.build(
    factory.addGetMany<PermissionApplicationInfosDTO, PermissionApplicationInfos, PermissionApplicationFilter>(PERMISSION_APPLICATIONS_URL, PermissionApplicationInfos),
    ServiceFactory.addCustom("getCategories",
        (axios, filter?: PermissionApplicationCategoriesFilter) => axios.get(PERMISSION_APPLICATION_CATEGORIES_URL(), { params: filter }),
        (dtos: PermissionApplicationCategoryDTO[]) => dtos.map(dto => new PermissionApplicationCategory(dto))),
    factory.addNotify()    
));

export const usePermissionApplications = ComposableFactory.getMany(PermissionApplicationServiceFactory);
export const usePermissionApplicationCategories = ComposableFactory.custom(PermissionApplicationServiceFactory.getCategories);
