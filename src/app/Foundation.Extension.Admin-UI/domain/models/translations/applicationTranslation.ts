import { SpreadsheetColumnDefinition } from "../applications/spreadsheetColumn";

export class ApplicationTranslation {
    id: string;
    translationCode: string;
    languageCode: string;
    value: string;

    constructor(params: ApplicationTranslationDTO) {
        this.id = params.id;
        this.translationCode = params.translationCode
        this.languageCode = params.languageCode;
        this.value = params.value;
    }
}

export interface ApplicationTranslationDTO {
    id: string;
    translationCode: string;
    languageCode: string;
    value: string;
}

export interface UpdateApplicationTranslation {
    translations: UpdateApplicationTranslationLanguage[];
}


export interface UpdateApplicationTranslationLanguage {
    translationCode: string;
    languageCode: string;
    value: string;
}

export interface DownloadApplicationTranslations {
    fileName: string;
}

export interface UploadApplicationTranslations {
    file: File;
    languages: SpreadsheetColumnDefinition[];
}

export interface ApplicationTranslationsFilter {
    languageCode?: string;
    translationCode?: string;
}