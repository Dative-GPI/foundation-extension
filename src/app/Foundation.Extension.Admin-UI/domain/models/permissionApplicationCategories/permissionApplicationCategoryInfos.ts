export class PermissionApplicationCategoryInfos {
  id: string;
  label: string;
  prefix: string;

  constructor(params: PermissionApplicationCategoryInfosDTO) {
    this.id = params.id;
    this.label = params.label;
    this.prefix = params.prefix;
  }
}

export interface PermissionApplicationCategoryInfosDTO {
  id: string;
  label: string;
  prefix: string;
}