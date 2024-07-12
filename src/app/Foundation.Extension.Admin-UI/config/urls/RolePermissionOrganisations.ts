import { BASE_URL } from "./base";

export const ROLE_PERMISSION_ORGANISATIONS_URL = `${BASE_URL}/role-organisations`;
export const ROLE_PERMISSION_ORGANISATION_URL = (id: string) => `${ROLE_PERMISSION_ORGANISATIONS_URL}/${encodeURIComponent(id)}`;