import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

import { ServiceAccountRoleOrganisationDetails, type ServiceAccountRoleOrganisationDetailsDTO, type UpdateServiceAccountRoleOrganisationDTO } from "../domain";
import { SERVICE_ACCOUNT_ROLE_ORGANISATION_URL } from "../config";

const ServiceAccountRoleOrganisationServiceFactory = new ServiceFactory<ServiceAccountRoleOrganisationDetailsDTO, ServiceAccountRoleOrganisationDetails>("extensionServiceAccountRoleOrganisation", ServiceAccountRoleOrganisationDetails).create(factory => factory.build(
  factory.addGet(SERVICE_ACCOUNT_ROLE_ORGANISATION_URL),
  factory.addUpdate<UpdateServiceAccountRoleOrganisationDTO>(SERVICE_ACCOUNT_ROLE_ORGANISATION_URL),
  factory.addNotify()
));

export const useServiceAccountRoleOrganisation = ComposableFactory.get(ServiceAccountRoleOrganisationServiceFactory);
export const useUpdateServiceAccountRoleOrganisation = ComposableFactory.update(ServiceAccountRoleOrganisationServiceFactory);