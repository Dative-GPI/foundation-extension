import { BASE_URL } from "./base";
import { urlFactory } from "./urlFactory";

export const ORGANISATIONS_URL = `${BASE_URL}/organisations`;
export const ORGANISATION_URL = urlFactory(orgId => `${ORGANISATIONS_URL}/${encodeURIComponent(orgId)}`); 
