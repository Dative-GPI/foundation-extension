import type { UserOrganisationTableInfosDTO } from "./userOrganisationTableInfos";
import { UserOrganisationTableInfos } from "./userOrganisationTableInfos";
import type { UpdateUserOrganisationColumnDTO } from "../userOrganisationColumns/userOrganisationColumnDetails";

export class UserOrganisationTableDetails extends UserOrganisationTableInfos {
    constructor(dto: UserOrganisationTableDetailsDTO) {
        super(dto);
    }
}

export interface UserOrganisationTableDetailsDTO extends UserOrganisationTableInfosDTO {
}

export interface UpdateUserOrganisationTableDTO {
    mode: "table" | "iterator";
    rowsPerPage: number;
    sortByKey: string | null;
    sortByOrder: string | null;
    columns: UpdateUserOrganisationColumnDTO[];
}