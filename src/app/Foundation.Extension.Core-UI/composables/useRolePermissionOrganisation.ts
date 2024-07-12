import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

import { ROLE_ORGANISATION_URL } from "../config";

import type { RolePermissionOrganisationDetailsDTO, UpdateRolePermissionOrganisationDTO } from "../domain";
import { RolePermissionOrganisationDetails } from "../domain";

const RoleOrganisationServiceFactory = new ServiceFactory<RolePermissionOrganisationDetailsDTO, RolePermissionOrganisationDetails>("role-permissions", RolePermissionOrganisationDetails)
    .create(f => f.build(
        f.addGet(ROLE_ORGANISATION_URL),
        f.addUpdate<UpdateRolePermissionOrganisationDTO>(ROLE_ORGANISATION_URL),
        f.addNotify()
    ));

export const useUpdateRolePermissionOrganisation = ComposableFactory.update(RoleOrganisationServiceFactory);
export const useRolePermissionOrganisation = ComposableFactory.get(RoleOrganisationServiceFactory);
