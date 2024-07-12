import { BASE_URL } from "./base";

export const APPLICATION_LANGUAGES_URL = `/api/foundation/admin/v1/application-languages`;

export const TRANSLATIONS_URL = `${BASE_URL}/translations`;
export const APPLICATION_TRANSLATIONS_URL = `${BASE_URL}/application-translations`;
export const CURRENT_USER_TRANSLATIONS_URL = `/api/translations`;

export const UPDATE_TRANSLATION_DRAWER_URL = (translation: string) => `/admin/translations/${translation}/drawer`;
export const IMPORT_TRANSLATIONS_DRAWER_URL = `/admin/translations/import-drawer`;

export const APPLICATION_TRANSLATION_URL = (translationCode: string) => `${APPLICATION_TRANSLATIONS_URL}/${translationCode}`;

export const APPLICATION_TRANSLATIONS_WORKBOOK_URL = `${APPLICATION_TRANSLATIONS_URL}/workbook`;
