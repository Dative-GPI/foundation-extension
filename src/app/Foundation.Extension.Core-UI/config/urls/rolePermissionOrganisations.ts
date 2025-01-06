import { ORGANISATION_URL } from "./organisation";

export const ROLE_PERMISSION_ORGANISATIONS_URL = () => `${ORGANISATION_URL()}/role-permission-organisations`;
export const ROLE_PERMISSION_ORGANISATION_URL = (roleId: string) => `${ROLE_PERMISSION_ORGANISATIONS_URL()}/${encodeURIComponent(roleId)}`;