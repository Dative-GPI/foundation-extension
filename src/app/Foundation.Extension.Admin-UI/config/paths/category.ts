import { BASE_PATH } from "./application";

export const CATEGORY_PATH = `${BASE_PATH}/category`;
export const CREATE_CATEGORY_DRAWER_PATH = `${CATEGORY_PATH}/drawer`;
export const UPDATE_CATEGORY_DRAWER_PATH = (categoryId: string) => `${CREATE_CATEGORY_DRAWER_PATH}/${encodeURIComponent(categoryId)}`;