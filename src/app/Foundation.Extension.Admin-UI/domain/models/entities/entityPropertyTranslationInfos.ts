export class EntityPropertyTranslationInfos {
    get id(): string {
        return this.entityPropertyId + this.languageCode
    }

    entityPropertyId: string;

    label: string;
    categoryLabel: string;

    languageCode: string;

    constructor(params: EntityPropertyTranslationDTO) {
        this.entityPropertyId = params.entityPropertyId;
        this.label = params.label;
        this.categoryLabel = params.categoryLabel;
        this.languageCode = params.languageCode;
    }
}

export interface EntityPropertyTranslationDTO {
    id: string;

    entityPropertyId: string;

    label: string;
    categoryLabel: string;

    languageCode: string;
}

export interface UpdateEntityPropertyTranslation {
    languageCode: string;
    label: string;
    categoryLabel: string;
}

export interface DownloadEntityPropertyTranslations {
    fileName: string;
}

export interface UploadEntityPropertyTranslations {
    file: File;
    labels: SpreadsheetColumnDefinition[];
    categories: SpreadsheetColumnDefinition[];
}

export interface SpreadsheetColumnDefinition {
    index: number;
    languageCode: string;
}

export interface EntityPropertyTranslationsFilter {
    translationCode?: string;
    prefix?: string;
}