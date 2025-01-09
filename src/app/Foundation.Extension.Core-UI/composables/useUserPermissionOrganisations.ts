import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

import { UserPermissionOrganisationDetails, type UserPermissionOrganisationDetailsDTO } from "../domain";
import { USER_PERMISSION_ORGANISATION_URL } from "../config";
import { type UserType } from "../domain/enums";

const UserPermissionOrganisationServiceFactory = new ServiceFactory<UserPermissionOrganisationDetailsDTO, UserPermissionOrganisationDetails>("user-permissions", UserPermissionOrganisationDetails).create(factory => factory.build(
  ServiceFactory.addCustom("getUserPermissionOrganisation", (axios, userId: string, userType: UserType) => axios.get(USER_PERMISSION_ORGANISATION_URL(userId, userType)), (dto: UserPermissionOrganisationDetailsDTO) => new UserPermissionOrganisationDetails(dto)),
));

export const useUserPermissionOrganisation = ComposableFactory.custom(UserPermissionOrganisationServiceFactory.getUserPermissionOrganisation);