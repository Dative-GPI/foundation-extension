import { ORGANISATION_URL } from "./organisation";

export const SERVICE_ACCOUNT_ROLE_ORGANISATIONS_URL = () => `${ORGANISATION_URL()}/service-account-role-organisations`;
export const SERVICE_ACCOUNT_ROLE_ORGANISATION_URL = (serviceAccountRoleOrganisationId: string) => `${SERVICE_ACCOUNT_ROLE_ORGANISATIONS_URL()}/${encodeURIComponent(serviceAccountRoleOrganisationId)}`;