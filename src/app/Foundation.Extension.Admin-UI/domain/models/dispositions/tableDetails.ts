import {ColumnDTO, Column} from "./column";

export class TableDetails {
    id: string;
    code: string;
    label: string;
    columns: Column[];

    constructor(params: TableDetailsDTO) {
        this.id = params.id;
        this.code = params.code;
        this.label = params.label;
        this.columns = params.columns.map(c => new Column(c));
    }
}

export interface TableDetailsDTO {
    id: string;
    code: string;
    label: string;
    columns: ColumnDTO[]
}
