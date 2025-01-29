import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

import { UserOrganisationDetails, type UserOrganisationDetailsDTO } from "../domain";
import { USER_ORGANISATION_CURRENT_URL, USER_ORGANISATION_URL } from "../config";

const UserOrganisationServiceFactory = new ServiceFactory<UserOrganisationDetailsDTO, UserOrganisationDetails>("extensionUserOrganisation", UserOrganisationDetails).create(factory => factory.build(
  factory.addGet(USER_ORGANISATION_URL),
  ServiceFactory.addCustom("getCurrent", (axios) => axios.get(USER_ORGANISATION_CURRENT_URL()), (dto: UserOrganisationDetailsDTO) => new UserOrganisationDetails(dto)),
  factory.addNotify()
));

export const useUserOrganisation = ComposableFactory.get(UserOrganisationServiceFactory);
export const useCurrentUserOrganisation = ComposableFactory.custom(UserOrganisationServiceFactory.getCurrent);