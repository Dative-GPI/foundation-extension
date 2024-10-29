import { CORE_PATH } from ".";

export const REMOVE_ENTITIES_DIALOG_PATH = (dialogId?: string) => `${CORE_PATH()}/dialogs/remove?dialogId=${dialogId}`;
export const CALENDAR_TWIN_DIALOG_PATH = (dialogId?: string) => `${CORE_PATH()}/dialogs/calendar-twin?dialogId=${dialogId}`;