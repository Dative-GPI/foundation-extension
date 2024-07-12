import { BASE_URL } from "./base";

export const PERMISSION_ORGANISATIONS_URL = () => `${BASE_URL}/permission-organisations`;

export const CURRENT_USER_PERMISSIONS_URL = () => `${PERMISSION_ORGANISATIONS_URL()}/current`;
export const PERMISSION_ORGANISATION_CATEGORIES_URL = () => `${BASE_URL}/permission-organisation-categories`;
