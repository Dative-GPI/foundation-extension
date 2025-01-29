import { ORGANISATION_URL } from "./organisation";

export const PERMISSION_ORGANISATIONS_URL = () => `${ORGANISATION_URL()}/permission-organisations`;

export const PERMISSION_ORGANISATIONS_CURRENT_URL = () => `${PERMISSION_ORGANISATIONS_URL()}/current`;