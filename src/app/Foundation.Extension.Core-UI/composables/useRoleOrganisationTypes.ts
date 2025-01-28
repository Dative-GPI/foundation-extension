import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

import { RoleOrganisationTypeDetails, type RoleOrganisationTypeDetailsDTO } from "../domain";
import { ROLE_ORGANISATION_TYPE_URL } from "../config";

const RoleOrganisationTypeServiceFactory = new ServiceFactory<RoleOrganisationTypeDetailsDTO, RoleOrganisationTypeDetails>("roleOrganisationType", RoleOrganisationTypeDetails).create(factory => factory.build(
  factory.addGet(ROLE_ORGANISATION_TYPE_URL),
  factory.addNotify()
));

export const useRoleOrganisationType = ComposableFactory.get(RoleOrganisationTypeServiceFactory);