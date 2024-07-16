import { BASE_URL } from "./base";
import { urlFactory } from "@dative-gpi/foundation-core-services/config/urls/urlFactory";

export const ORGANISATIONS_URL = `${BASE_URL}/organisations`;
export const ORGANISATION_URL = urlFactory(orgId => `${ORGANISATIONS_URL}/${encodeURIComponent(orgId)}`); 
