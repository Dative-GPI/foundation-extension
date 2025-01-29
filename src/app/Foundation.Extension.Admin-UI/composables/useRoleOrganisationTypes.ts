import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

import { RoleOrganisationTypeDetails, type RoleOrganisationTypeDetailsDTO, type UpdateRoleOrganisationTypeDTO } from "../domain";
import { ROLE_ORGANISATION_TYPE_URL } from "../config";

const RoleOrganisationTypeServiceFactory = new ServiceFactory<RoleOrganisationTypeDetailsDTO, RoleOrganisationTypeDetails>("roleOrganisationTypes", RoleOrganisationTypeDetails).create(factory => factory.build(
  factory.addGet(ROLE_ORGANISATION_TYPE_URL),
  factory.addUpdate<UpdateRoleOrganisationTypeDTO>(ROLE_ORGANISATION_TYPE_URL),
  factory.addNotify()
));

export const useRoleOrganisationType = ComposableFactory.get(RoleOrganisationTypeServiceFactory);
export const useUpdateRoleOrganisationType = ComposableFactory.update(RoleOrganisationTypeServiceFactory);