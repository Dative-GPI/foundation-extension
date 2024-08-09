import { GATEWAY_URL } from "./base";

export const IMAGES_URL = () => `${GATEWAY_URL()}/images`;

export const IMAGE_URL = (imageId: string) => `${IMAGES_URL()}/${encodeURIComponent(imageId)}`;
export const IMAGE_RAW_URL = (imageId: string) => `${IMAGE_URL(imageId)}/raw`;
export const IMAGE_RAW_SOURCE_URL = (imageId: string, jwt: string) => `${IMAGE_RAW_URL(imageId)}?access_token=${jwt}`;

export const IMAGE_THUMBNAIL_URL = (imageId: string) => `${IMAGE_URL(imageId)}/thumbnail`;
