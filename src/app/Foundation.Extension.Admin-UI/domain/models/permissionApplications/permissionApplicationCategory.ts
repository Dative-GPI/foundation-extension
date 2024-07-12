export class PermissionApplicationCategory {
    label: string;
    prefix: string;

    constructor(params: PermissionApplicationCategoryDTO) {
        this.label = params.label;
        this.prefix = params.prefix;
    }
}

export interface PermissionApplicationCategoryDTO {
    label: string;
    prefix: string;
}


export interface PermissionApplicationCategoriesFilter {
    search?: string
}