import { type Ref } from "vue";

import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

import { RolePermissionOrganisationDetails, type RolePermissionOrganisationDetailsDTO, type UpdateRolePermissionOrganisationDTO } from "../domain";
import { SERVICE_ACCOUNT_ROLE_ORGANISATION_URL, ROLE_ORGANISATION_TYPE_URL, ROLE_ORGANISATION_URL } from "../config";

const RolePermissionOrganisationServiceFactory = new ServiceFactory<RolePermissionOrganisationDetailsDTO, RolePermissionOrganisationDetails>("role-permissions", RolePermissionOrganisationDetails).create(factory => factory.build(
  ServiceFactory.addCustom("getServiceAccountRoleOrganisation", (axios, roleId: string) => axios.get(SERVICE_ACCOUNT_ROLE_ORGANISATION_URL(roleId)), (dto: RolePermissionOrganisationDetailsDTO) => new RolePermissionOrganisationDetails(dto)),
  ServiceFactory.addCustom("getRoleOrganisationType", (axios, roleId: string) => axios.get(ROLE_ORGANISATION_TYPE_URL(roleId)), (dto: RolePermissionOrganisationDetailsDTO) => new RolePermissionOrganisationDetails(dto)),
  ServiceFactory.addCustom("getRoleOrganisation", (axios, roleId: string) => axios.get(ROLE_ORGANISATION_URL(roleId)), (dto: RolePermissionOrganisationDetailsDTO) => new RolePermissionOrganisationDetails(dto)),
  factory.addNotify(notifyService => ({
    ...ServiceFactory.addCustom("updateServiceAccountRoleOrganisation", (axios, roleId: string, payload: UpdateRolePermissionOrganisationDTO) => axios.post(SERVICE_ACCOUNT_ROLE_ORGANISATION_URL(roleId), payload), (dto: RolePermissionOrganisationDetailsDTO) => {
      const result = new RolePermissionOrganisationDetails(dto);
      notifyService.notify("update", result);
      return result;
    }),
    ...ServiceFactory.addCustom("updateRoleOrganisation", (axios, roleId: string, payload: UpdateRolePermissionOrganisationDTO) => axios.post(ROLE_ORGANISATION_URL(roleId), payload), (dto: RolePermissionOrganisationDetailsDTO) => {
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

export const useServiceAccountRoleOrganisation = ComposableFactory.custom(RolePermissionOrganisationServiceFactory.getServiceAccountRoleOrganisation, trackRolePermissionOrganisation);
export const useRoleOrganisationType = ComposableFactory.custom(RolePermissionOrganisationServiceFactory.getRoleOrganisationType, trackRolePermissionOrganisation);
export const useRoleOrganisation = ComposableFactory.custom(RolePermissionOrganisationServiceFactory.getRoleOrganisation, trackRolePermissionOrganisation);
export const useUpdateServiceAccountRoleOrganisation = ComposableFactory.custom(RolePermissionOrganisationServiceFactory.updateServiceAccountRoleOrganisation);
export const useUpdateRoleOrganisation = ComposableFactory.custom(RolePermissionOrganisationServiceFactory.updateRoleOrganisation);

