import { BASE_URL } from "./base";

export const ROLE_ORGANISATION_TYPES_URL = `${BASE_URL}/role-organisation-types`;
export const ROLE_ORGANISATION_TYPE_URL = (roleOrganisationTypeId: string) => `${ROLE_ORGANISATION_TYPES_URL}/${encodeURIComponent(roleOrganisationTypeId)}`;