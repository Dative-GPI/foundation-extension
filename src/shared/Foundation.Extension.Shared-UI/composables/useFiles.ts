import { FILE_URL } from "../config/urls";

import { useAppAuthToken, useRouting } from "@dative-gpi/foundation-shared-services/composables";

export const useFiles = () => {
  const { authToken } = useAppAuthToken();
  const { openTab } = useRouting();

  const downloadFile = (id: string): void => {
    openTab(FILE_URL(id, authToken.value));
  };

  const readFile = (file: File): Promise<string | ArrayBuffer | null> => {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.addEventListener("load", (fileEv) => {
        resolve(fileEv.target && fileEv.target.result);
      });
      reader.addEventListener("error", (fileEv) => {
        reject(fileEv);
      });
      reader.readAsDataURL(file);
    });
  };

  return {
    downloadFile,
    readFile
  };
}