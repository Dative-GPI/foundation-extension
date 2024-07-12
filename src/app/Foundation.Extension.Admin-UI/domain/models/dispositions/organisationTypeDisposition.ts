export class OrganisationTypeDisposition {
    id: string;
    columnId: string;
    index: number;
    hidden: boolean;
    disabled: boolean;

    constructor(params: OrganisationTypeDispositionDTO) {
        this.id = params.id;
        this.columnId = params.columnId;
        this.index = params.index;
        this.hidden = params.hidden;
        this.disabled = params.disabled;
    }
}

export interface OrganisationTypeDispositionDTO {
    id: string;
    columnId: string;
    index: number;
    hidden: boolean;
    disabled: boolean;
}

export interface UpdateOrganisationTypeDispositionDTO {
    columnId: string;
    disabled: boolean;
    hidden: boolean;
    index: number;
}