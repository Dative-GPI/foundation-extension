import type { UserOrganisationColumnInfosDTO } from "./userOrganisationColumnInfos";
import { UserOrganisationColumnInfos } from "./userOrganisationColumnInfos";

export class UserOrganisationColumnDetails extends UserOrganisationColumnInfos {
    constructor(dto: UserOrganisationColumnInfosDTO) {
        super(dto);
    }
}

export interface UserOrganisationColumnDetailsDTO extends UserOrganisationColumnInfosDTO {
}

export interface UpdateUserOrganisationColumnDTO {
    columnId: string;
    index: number;
    hidden: boolean;
}