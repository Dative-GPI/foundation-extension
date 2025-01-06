import { type Ref } from "vue";

import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

import { RolePermissionOrganisationDetails, type RolePermissionOrganisationDetailsDTO, type UpdateRolePermissionOrganisationDTO } from "../domain";
import { ROLE_PERMISSION_ORGANISATION_URL } from "../config";

const RolePermissionOrganisationServiceFactory = new ServiceFactory<RolePermissionOrganisationDetailsDTO, RolePermissionOrganisationDetails>("role-permissions", RolePermissionOrganisationDetails).create(factory => factory.build(
  ServiceFactory.addCustom("getRolePermissionOrganisation", (axios, roleId: string) => axios.get(ROLE_PERMISSION_ORGANISATION_URL(roleId)), (dto: RolePermissionOrganisationDetailsDTO) => new RolePermissionOrganisationDetails(dto)),
  factory.addNotify(notifyService => ({
    ...ServiceFactory.addCustom("updateRolePermissionOrganisation", (axios, roleId: string, payload: UpdateRolePermissionOrganisationDTO) => axios.post(ROLE_PERMISSION_ORGANISATION_URL(roleId), payload), (dto: RolePermissionOrganisationDetailsDTO) => {
      const result = new RolePermissionOrganisationDetails(dto);
      notifyService.notify("update", result);
      return result;
    })
  }))
));

export const useTrackRolePermissionOrganisation = ComposableFactory.track(RolePermissionOrganisationServiceFactory);
export const useTrackRolePermissionOrganisationRef = ComposableFactory.trackRef(RolePermissionOrganisationServiceFactory);

const trackRolePermissionOrganisation = () => {
  const { track } = useTrackRolePermissionOrganisationRef();

  return (rolePermissionOrganisation: Ref<RolePermissionOrganisationDetails>) => {
    track(rolePermissionOrganisation);
  }
}

export const useRolePermissionOrganisation = ComposableFactory.custom(RolePermissionOrganisationServiceFactory.getRolePermissionOrganisation, trackRolePermissionOrganisation);
export const useUpdateRolePermissionOrganisation = ComposableFactory.custom(RolePermissionOrganisationServiceFactory.updateRolePermissionOrganisation);