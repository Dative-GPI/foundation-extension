import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

import { RoleOrganisationDetails, type RoleOrganisationDetailsDTO, type UpdateRoleOrganisationDTO } from "../domain";
import { ROLE_ORGANISATION_URL } from "../config";

const RoleOrganisationServiceFactory = new ServiceFactory<RoleOrganisationDetailsDTO, RoleOrganisationDetails>("roleOrganisation", RoleOrganisationDetails).create(factory => factory.build(
  factory.addGet(ROLE_ORGANISATION_URL),
  factory.addUpdate<UpdateRoleOrganisationDTO>(ROLE_ORGANISATION_URL),
  factory.addNotify()
));

export const useRoleOrganisation = ComposableFactory.get(RoleOrganisationServiceFactory);
export const useUpdateRoleOrganisation = ComposableFactory.update(RoleOrganisationServiceFactory);