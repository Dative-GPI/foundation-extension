import { GATEWAY_URL } from "./base";

export const FILES_URL = () => `${GATEWAY_URL()}/files`;
export const FILE_URL = (fileId: string, accessToken?: string) => {
  let url = `${FILES_URL()}/${encodeURIComponent(fileId)}`;
  if (accessToken) {
    url += `?access_token=${encodeURIComponent(accessToken)}`;
  }
  return url;
};