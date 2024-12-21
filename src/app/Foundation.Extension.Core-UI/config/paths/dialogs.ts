import { CORE_PATH } from ".";

export const REMOVE_ENTITIES_DIALOG_PATH = (dialogId?: string) => `${CORE_PATH()}/dialogs/remove?dialogId=${dialogId}`;
export const CALENDAR_TWIN_DIALOG_PATH = (dialogId?: string) => `${CORE_PATH()}/dialogs/calendar-twin?dialogId=${dialogId}`;

export const UPDATE_SERVICE_ACCOUNT_ROLE_ORGANISATION_DIALOG_PATH = (roleId: string, dialogId?: string) => `${CORE_PATH()}/dialogs/service-account-role-organisations/${encodeURIComponent(roleId)}/update?dialogId=${dialogId}`;
export const UPDATE_ROLE_ORGANISATION_DIALOG_PATH = (roleId: string, dialogId?: string) => `${CORE_PATH()}/dialogs/role-organisations/${encodeURIComponent(roleId)}/update?dialogId=${dialogId}`;