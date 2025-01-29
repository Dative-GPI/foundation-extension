import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

import { ServiceAccountOrganisationDetails, type ServiceAccountOrganisationDetailsDTO } from "../domain";
import { SERVICE_ACCOUNT_ORGANISATION_URL } from "../config";

const ServiceAccountOrganisationServiceFactory = new ServiceFactory<ServiceAccountOrganisationDetailsDTO, ServiceAccountOrganisationDetails>("serviceAccountOrganisation", ServiceAccountOrganisationDetails).create(factory => factory.build(
  factory.addGet(SERVICE_ACCOUNT_ORGANISATION_URL),
  factory.addNotify()
));

export const useServiceAccountOrganisation = ComposableFactory.get(ServiceAccountOrganisationServiceFactory);