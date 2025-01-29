import { ServiceFactory, ComposableFactory } from "@dative-gpi/bones-ui";

import { PermissionApplicationCategoryInfos, type PermissionApplicationCategoryInfosDTO} from "../domain";
import { PERMISSION_APPLICATION_CATEGORIES_URL } from "../config";

const PermissionApplicationCategoryServiceFactory = new ServiceFactory<PermissionApplicationCategoryInfosDTO, PermissionApplicationCategoryInfos>("extensionPermissionApplicationCategory", PermissionApplicationCategoryInfos).create(factory => factory.build(
  factory.addGetMany(PERMISSION_APPLICATION_CATEGORIES_URL, PermissionApplicationCategoryInfos),
  factory.addNotify()    
));

export const usePermissionApplicationCategories = ComposableFactory.getMany(PermissionApplicationCategoryServiceFactory);