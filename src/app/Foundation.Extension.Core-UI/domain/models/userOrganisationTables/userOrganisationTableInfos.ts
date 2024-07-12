import type { UserOrganisationColumnInfosDTO } from "../userOrganisationColumns/userOrganisationColumnInfos";
import { UserOrganisationColumnInfos } from "../userOrganisationColumns/userOrganisationColumnInfos";

export class UserOrganisationTableInfos {
    id: string;
    code: string;
    columns: UserOrganisationColumnInfos[];

    // Depends on [OrganisationType, UserOrganisation]
    mode: "table" | "iterator";
    rowsPerPage: -1 | 10 | 30;
    sortByKey: string | null;
    sortByOrder: "asc" | "desc" | null;

    constructor(dto: UserOrganisationTableInfosDTO) {
        this.id = dto.id;
        this.code = dto.code;
        this.mode = dto.mode;
        this.rowsPerPage = dto.rowsPerPage;
        this.sortByKey = dto.sortByKey;
        this.sortByOrder = dto.sortByOrder;
        this.columns = dto.columns?.map(dto => new UserOrganisationColumnInfos(dto)) ?? [];
    }
}

export interface UserOrganisationTableInfosDTO {
    id: string;
    code: string;
    mode: "table" | "iterator";
    rowsPerPage: -1 | 10 | 30;
    sortByKey: string | null;
    sortByOrder: "asc" | "desc" | null;
    columns: UserOrganisationColumnInfosDTO[];
}