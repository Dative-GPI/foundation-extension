export class ServiceAccountRoleOrganisationDetails {
    id: string;
    permissionIds: string[];
  
    constructor(payload: ServiceAccountRoleOrganisationDetailsDTO) {
      this.id = payload.id;
      this.permissionIds = payload.permissionIds;
    }
  }
  
  export interface ServiceAccountRoleOrganisationDetailsDTO {
    id: string;
    permissionIds: string[];
  }
  
  export interface UpdateServiceAccountRoleOrganisationDTO {
    permissionIds: string[];
  }