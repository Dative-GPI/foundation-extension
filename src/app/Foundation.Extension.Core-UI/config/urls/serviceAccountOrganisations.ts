import { ORGANISATION_URL } from "./organisation";

export const SERVICE_ACCOUNT_ORGANISATIONS_URL = () => `${ORGANISATION_URL()}/service-account-organisations`;
export const SERVICE_ACCOUNT_ORGANISATION_URL = (serviceAccountOrganisationId: string) => `${SERVICE_ACCOUNT_ORGANISATIONS_URL()}/${encodeURIComponent(serviceAccountOrganisationId)}`;