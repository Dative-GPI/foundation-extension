import { type UserType } from "../../domain/enums";

import { ORGANISATION_URL } from "./organisation";

export const USER_PERMISSION_ORGANISATIONS_URL = () => `${ORGANISATION_URL()}/user-permission-organisations`;
export const USER_PERMISSION_ORGANISATION_URL = (userId: string, userType: UserType) => `${USER_PERMISSION_ORGANISATIONS_URL()}/${encodeURIComponent(userId)}/${encodeURIComponent(userType)}`;