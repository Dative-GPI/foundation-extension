import { CORE_PATH } from ".";

export const DIALOG_REMOVE = (dialogId: string) => `${CORE_PATH()}/dialogs/remove?dialogId=${dialogId}`;