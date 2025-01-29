export class PermissionOrganisationCategoryInfos {
  id: string;
  label: string;
  prefix: string;

  constructor(params: PermissionOrganisationCategoryInfosDTO) {
    this.id = params.id;
    this.label = params.label;
    this.prefix = params.prefix;
  }
}

export interface PermissionOrganisationCategoryInfosDTO {
  id: string;
  label: string;
  prefix: string;
}