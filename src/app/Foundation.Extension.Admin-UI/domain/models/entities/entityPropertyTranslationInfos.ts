import { SpreadsheetColumnDefinition } from "../applications/spreadsheetColumn";

export class EntityPropertyTranslationInfos {
    get id(): string {
        return this.entityPropertyId + this.languageCode
    }

    entityPropertyId: string;

    label: string;

    languageCode: string;

    constructor(params: EntityPropertyTranslationDTO) {
        this.entityPropertyId = params.entityPropertyId;
        this.label = params.label;
        this.languageCode = params.languageCode;
    }
}

export interface EntityPropertyTranslationDTO {
    id: string;

    entityPropertyId: string;

    label: string;

    languageCode: string;
}

export interface UpdateEntityPropertyTranslation {
    languageCode: string;
    label: string;
}

export interface DownloadEntityPropertyTranslations {
    fileName: string;
}

export interface UploadEntityPropertyTranslations {
    file: File;
    languages: SpreadsheetColumnDefinition[];
}

export interface EntityPropertyTranslationsFilter {
    translationCode?: string;
    prefix?: string;
}