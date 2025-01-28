import { ORGANISATION_URL } from "./organisation";

export const ROLE_ORGANISATIONS_URL = () => `${ORGANISATION_URL()}/role-organisations`;
export const ROLE_ORGANISATION_URL = (roleOrganisationId: string) => `${ROLE_ORGANISATIONS_URL()}/${encodeURIComponent(roleOrganisationId)}`;