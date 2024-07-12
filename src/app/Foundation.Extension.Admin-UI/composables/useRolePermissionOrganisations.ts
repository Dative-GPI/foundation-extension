import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

import type { RolePermissionOrganisationDetailsDTO, UpdateRolePermissionOrganisation } from "../domain";
import { RolePermissionOrganisationDetails } from "../domain";

import { ROLE_PERMISSION_ORGANISATION_URL } from "../config/urls/RolePermissionOrganisations";

const RolePermissionServiceFactory = new ServiceFactory<RolePermissionOrganisationDetailsDTO, RolePermissionOrganisationDetails>("rolePermissionOrganisationType", RolePermissionOrganisationDetails)
    .create(f => f.build(
        f.addGet(ROLE_PERMISSION_ORGANISATION_URL),
        f.addUpdate<UpdateRolePermissionOrganisation>(ROLE_PERMISSION_ORGANISATION_URL),
        f.addNotify()
    ));

export const useRolePermissionOrganisations = ComposableFactory.get(RolePermissionServiceFactory);
export const useUpdateRolePermissionOrganisations = ComposableFactory.custom(RolePermissionServiceFactory.update);

