import { CORE_PATH } from ".";

export const REMOVE_ENTITIES_DIALOG_PATH = (dialogId?: string) => `${CORE_PATH()}/dialogs/remove?dialogId=${dialogId}`;
export const CALENDAR_TWIN_DIALOG_PATH = (dialogId?: string) => `${CORE_PATH()}/dialogs/calendar-twin?dialogId=${dialogId}`;

export const UPDATE_ROLE_PERMISSION_ORGANISATION_DIALOG_PATH = (roleId: string, dialogId?: string) => `${CORE_PATH()}/dialogs/role-organisation-permissions/${encodeURIComponent(roleId)}/update?dialogId=${dialogId}`;