import { GATEWAY_URL } from "./base";

export const TRANSLATIONS_URL = () => `${GATEWAY_URL()}/translations`;
export const TRANSLATIONS_LANGUAGE_URL = (languageCode: string) => `${TRANSLATIONS_URL()}/${encodeURIComponent(languageCode)}`;