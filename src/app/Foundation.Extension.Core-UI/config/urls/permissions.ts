import { ORGANISATION_URL } from "./organisation";

export const PERMISSIONS_URL = () => `${ORGANISATION_URL()}/permissions`;

export const CURRENT_USER_PERMISSIONS_URL = () => `${PERMISSIONS_URL()}/current`;
export const PERMISSION_CATEGORIES_URL = () => `${PERMISSIONS_URL()}/categories`;
