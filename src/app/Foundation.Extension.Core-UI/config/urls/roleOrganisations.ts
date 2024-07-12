import { ORGANISATION_URL } from "./organisation";

export const ROLE_ORGANISATIONS_URL = () => `${ORGANISATION_URL()}/roles`;
export const ROLE_ORGANISATION_URL = (roleId: string) => `${ROLE_ORGANISATIONS_URL()}/${encodeURIComponent(roleId)}`;
