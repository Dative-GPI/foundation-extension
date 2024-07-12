import { BASE_URL } from "./base";

export const USER_ORGANISATION_TABLES_URL = `${BASE_URL}/user-organisation-tables`;
export const USER_ORGANISATION_TABLE_URL = (tableCode: string) => `${USER_ORGANISATION_TABLES_URL}/${encodeURIComponent(tableCode)}`;