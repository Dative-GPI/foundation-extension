export class TableInfos {
    id: string;
    code: string;
    label: string;

    constructor(params: TableInfosDTO) {
        this.id = params.id;
        this.code = params.code;
        this.label = params.label;
    }
}

export interface TableInfosDTO {
    id: string;
    code: string;
    label: string;
}

export interface TableFilter {
    search?: string
}

