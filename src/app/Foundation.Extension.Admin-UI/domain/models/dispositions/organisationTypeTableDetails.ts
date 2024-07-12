import { OrganisationTypeDisposition, OrganisationTypeDispositionDTO, UpdateOrganisationTypeDispositionDTO } from "./organisationTypeDisposition";

export class OrganisationTypeTableDetails {
    tableId: string;
    organisationTypeId: string;
    dispositions: OrganisationTypeDisposition[];
    get id() {
        return `${this.organisationTypeId}-${this.tableId}`
    }


    constructor(params: OrganisationTypeTableDetailsDTO) {
        this.tableId = params.tableId;
        this.organisationTypeId = params.organisationTypeId;
        this.dispositions = params.dispositions.map(d => new OrganisationTypeDisposition(d));
    }
}

export interface OrganisationTypeTableDetailsDTO {
    tableId: string;
    organisationTypeId: string;
    dispositions: OrganisationTypeDispositionDTO[];
}

export interface UpdateOrganisationTypeTableDTO {
    dispositions: UpdateOrganisationTypeDispositionDTO[];
}

export interface OrganisationTypeTableFilter {
    organisationTypeId: string;
    tableId: string;
}
