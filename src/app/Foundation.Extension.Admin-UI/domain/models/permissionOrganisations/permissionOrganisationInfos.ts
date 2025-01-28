export class PermissionOrganisationInfos {
    id: string;
    code: string;
    label: string;

    constructor(params: PermissionOrganisationInfosDTO) {
        this.id = params.id;
        this.code = params.code;
        this.label = params.label;
    }
}

export interface PermissionOrganisationInfosDTO {
    id: string;
    code: string;
    label: string;
}

export interface PermissionOrganisationsFilter {
    search?: string
}