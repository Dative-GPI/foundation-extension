import { ORGANISATION_URL } from "./organisation";

export const ROLE_PERMISSION_ORGANISATIONS_URL = () => `${ORGANISATION_URL()}/role-permission-organisations`;
export const SERVICE_ACCOUNT_ROLE_ORGANISATION_URL = (roleId: string) => `${ROLE_PERMISSION_ORGANISATIONS_URL()}/service-account-role-organisation/${encodeURIComponent(roleId)}`;
export const ROLE_ORGANISATION_TYPE_URL = (roleId: string) => `${ROLE_PERMISSION_ORGANISATIONS_URL()}/role-organisation-type/${encodeURIComponent(roleId)}`;
export const ROLE_ORGANISATION_URL = (roleId: string) => `${ROLE_PERMISSION_ORGANISATIONS_URL()}/role-organisation/${encodeURIComponent(roleId)}`;