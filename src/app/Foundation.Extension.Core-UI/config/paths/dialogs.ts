import { CORE_PATH } from ".";

export const DIALOG_REMOVE = (dialogId: string) => `${CORE_PATH()}/dialogs/remove?dialogId=${dialogId}`;
export const CALENDAR_TWIN_DIALOG = (dialogId: string) => `${CORE_PATH()}/dialogs/calendar-twin?dialogId=${dialogId}`;