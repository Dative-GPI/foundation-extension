export class ServiceAccountOrganisationDetails {
  id: string;
  permissionIds: string[];

  constructor(payload: ServiceAccountOrganisationDetailsDTO) {
    this.id = payload.id;
    this.permissionIds = payload.permissionIds;
  }
}

export interface ServiceAccountOrganisationDetailsDTO {
  id: string;
  permissionIds: string[];
}