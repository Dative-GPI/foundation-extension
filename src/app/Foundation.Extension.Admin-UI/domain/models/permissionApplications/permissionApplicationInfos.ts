export class PermissionApplicationInfos {
  id: string;
  code: string;
  label: string;

  constructor(params: PermissionApplicationInfosDTO) {
    this.id = params.id;
    this.code = params.code;
    this.label = params.label;
  }
}

export interface PermissionApplicationInfosDTO {
  id: string;
  code: string;
  label: string;
}

export interface PermissionApplicationsFilter {
  search?: string
}