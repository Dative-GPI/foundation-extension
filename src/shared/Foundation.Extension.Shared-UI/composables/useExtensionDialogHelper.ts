import { useExtensionCommunicationBridge } from "./useExtensionCommunicationBridge";
import { dialogReadySchema, dialogSubmitSchema } from "../domain/dialogs";

export const useExtensionDialogHelper = () => {
  const { notify, subscribe } = useExtensionCommunicationBridge();

  const sendReady = () => {
    notify({
      messageType: "dialog-ready"
    });
  }

  const subscribeReady = (callback: () => void) => {
    subscribe(
      dialogReadySchema,
      location.href,
      callback
    );
  }

  const sendSubmit = () => {
    notify({
      messageType: "dialog-submit"
    });
  }

  const subscribeSubmit = (callback: () => void) => {
    subscribe(
      dialogSubmitSchema,
      location.href,
      callback
    );
  }

  const sendRemovePayload = (removeCurrent: number, removeTotal: number) => {
    notify({
      messageType: "dialog-remove",
      removeCurrent,
      removeTotal
    });
  }

  return {
    sendReady,
    sendSubmit,
    subscribeReady,
    subscribeSubmit,
    sendRemovePayload
  }
}

