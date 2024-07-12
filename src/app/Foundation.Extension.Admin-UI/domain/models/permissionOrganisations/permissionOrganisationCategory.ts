export class PermissionOrganisationCategory {
    label: string;
    prefix: string;

    constructor(params: PermissionOrganisationCategoryDTO) {
        this.label = params.label;
        this.prefix = params.prefix;
    }
}

export interface PermissionOrganisationCategoryDTO {
    label: string;
    prefix: string;
}