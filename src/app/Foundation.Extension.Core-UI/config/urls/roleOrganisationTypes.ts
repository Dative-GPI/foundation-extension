import { ORGANISATION_URL } from "./organisation";

export const ROLE_ORGANISATION_TYPES_URL = () => `${ORGANISATION_URL()}/role-organisation-types`;
export const ROLE_ORGANISATION_TYPE_URL = (roleOrganisationTypeId: string) => `${ROLE_ORGANISATION_TYPES_URL()}/${encodeURIComponent(roleOrganisationTypeId)}`;