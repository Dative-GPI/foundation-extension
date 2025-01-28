import { BASE_URL } from "./base";

export const ROLES_APPLICATION_URL = `${BASE_URL}/role-applications`;
export const ROLE_APPLICATION_URL = (roleApplicationId: string) => `${ROLES_APPLICATION_URL}/${encodeURIComponent(roleApplicationId)}`;
