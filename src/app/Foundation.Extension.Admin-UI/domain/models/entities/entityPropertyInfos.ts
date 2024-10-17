export class EntityPropertyInfos {
    id: string;
    code: string;
    entityType: string;

    labelDefault: string;

    parentId: string;

    value: string;

    constructor(params: EntityPropertyDTO) {
        this.id = params.id;
        this.code = params.code;
        this.entityType = params.entityType;
        this.labelDefault = params.labelDefault;
        this.value = params.value;
        this.parentId = params.parentId;
    }
}

export interface EntityPropertyDTO {
    id: string;
    code: string;
    entityType: string;

    parentId: string;
    labelDefault: string;

    value: string;
}