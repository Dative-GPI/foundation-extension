export class PermissionOrganisationTypeInfos {
    id: string;
    organisationTypeId: string;
    permissionLabel: string;
    permissionCode: string;
    permissionId: string;

    constructor(params: PermissionOrganisationTypeInfosDTO) {
        this.id = params.id;
        this.organisationTypeId = params.organisationTypeId;
        this.permissionLabel = params.permissionLabel;
        this.permissionCode = params.permissionCode;
        this.permissionId = params.permissionId;
    }
}

export interface PermissionOrganisationTypeInfosDTO {
    id: string;
    organisationTypeId: string;
    permissionLabel: string;
    permissionCode: string;
    permissionId: string;
}

export interface PermissionOrganisationTypeFilter {
    organisationTypeId?: string
    search?: string
}

export interface UpsertPermissionOrganisation {
    organisationTypeId: string;
    permissionIds: string[];
}

