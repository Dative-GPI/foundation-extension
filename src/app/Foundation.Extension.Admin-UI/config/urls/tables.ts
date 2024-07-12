import { BASE_URL } from "./base";

export const TABLES_URL = `${BASE_URL}/tables`;
export const TABLE_URL = (id: string) => `${TABLES_URL}/${encodeURIComponent(id)}`
export const TABLE_PROPERTIES_URL = (id: string) => `${TABLE_URL(id)}/properties`