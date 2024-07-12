import { BASE_URL } from "./base";

export const PERMISSION_APPLICATIONS_URL = () => `${BASE_URL}/permission-applications`
export const PERMISSION_APPLICATION_CATEGORIES_URL = () =>  "/api/admin/v1/permission-application-categories"
export const CURRENT_USER_PERMISSIONS_APPLICATION_URL = `${PERMISSION_APPLICATIONS_URL}/current`;