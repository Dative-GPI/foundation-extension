export const ENTITYPROPERTIES_URL = "/api/admin/v1/entity-properties";

export const ENTITYPROPERTY_TRANSLATIONS_URL = "/api/admin/v1/entity-property-translations";
export const ENTITYPROPERTY_TRANSLATION_URL = (id: string) => `${ENTITYPROPERTY_TRANSLATIONS_URL}/${encodeURIComponent(id)}`;
export const ENTITYPROPERTY_TRANSLATIONS_WORKBOOK_URL = `${ENTITYPROPERTY_TRANSLATIONS_URL}/workbook`;

export const UPDATE_ENTITY_PROPERTY_DRAWER_URL = (entityProperty: string) => `/admin/entitypropertytranslation/${entityProperty}/drawer`;