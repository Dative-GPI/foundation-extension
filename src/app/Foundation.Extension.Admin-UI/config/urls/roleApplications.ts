import { BASE_URL } from "./base";

export const ROLES_APPLICATION_URL = `${BASE_URL}/role-applications`;

export const ROLE_APPLICATION_URL = (id: string) => `${ROLES_APPLICATION_URL}/${encodeURIComponent(id)}`;

export const ROLE_APPLICATION_PERMISSIONS_URL = (id: string) => ` ${ROLE_APPLICATION_URL(id)}/permissions`;
