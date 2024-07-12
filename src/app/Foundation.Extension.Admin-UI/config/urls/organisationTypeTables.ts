import { BASE_URL } from "./base";

export const ORGANISATION_TYPE_TABLE_URL = (organisationTypeid: string, tableId: string) => `${BASE_URL}/organisation-types/${encodeURIComponent(organisationTypeid)}/tables/${encodeURIComponent(tableId)}`