export class Column {
    id: string;
    value: string;
    index: number;
    hidden: boolean;
    sortable: boolean;
    filterable: boolean;
    configurable: boolean;
    label: string;
    disabled: boolean;

    constructor(params: ColumnDTO) {
        this.id = params.id;
        this.value = params.value;
        this.index = params.index;
        this.hidden = params.hidden;
        this.sortable = params.sortable;
        this.filterable = params.filterable;
        this.configurable = params.configurable;
        this.label = params.label;
        this.disabled = params.disabled;
    }
}

export interface ColumnDTO {
    id: string;
    value: string;
    index: number;
    hidden: boolean;
    sortable: boolean;
    filterable: boolean;
    configurable: boolean;
    label: string;
    disabled: boolean;
}