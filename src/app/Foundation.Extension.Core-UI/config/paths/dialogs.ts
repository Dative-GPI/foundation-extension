import { CORE_PATH } from ".";

export const REMOVE_ENTITIES_DIALOG_PATH = (dialogId?: string) => `${CORE_PATH()}/dialogs/remove?dialogId=${dialogId}`;
export const CALENDAR_TWIN_DIALOG_PATH = (dialogId?: string) => `${CORE_PATH()}/dialogs/calendar-twin?dialogId=${dialogId}`;

export const UPDATE_ROLE_ORGANISATION_DIALOG_PATH = (roleOrganisationId: string, dialogId?: string) => `${CORE_PATH()}/dialogs/role-organisations/${encodeURIComponent(roleOrganisationId)}/update?dialogId=${dialogId}`;
export const UPDATE_SERVICE_ACCOUNT_ROLE_ORGANISATION_DIALOG_PATH = (serviceAccountRoleOrganisationId: string, dialogId?: string) => `${CORE_PATH()}/dialogs/service-account-role-organisations/${encodeURIComponent(serviceAccountRoleOrganisationId)}/update?dialogId=${dialogId}`;