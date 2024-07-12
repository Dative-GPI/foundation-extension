export class Language {
    id: string;
    code: string;
    label: string;
    icon: string;
    imageId: string;
    imageBlurHash: string;
    imageHeight: number;
    imageWidth: number;

    constructor(params: LanguageDTO) {
        this.id = params.id;
        this.code = params.code;
        this.label = params.label;
        this.icon = params.icon;
        this.imageId = params.imageId;
        this.imageBlurHash = params.imageBlurHash;
        this.imageHeight = params.imageHeight;
        this.imageWidth = params.imageWidth;
    }
}

export interface LanguageDTO {
    id: string;
    code: string;
    label: string;
    icon: string;
    imageId: string;
    imageBlurHash: string;
    imageHeight: number;
    imageWidth: number;
}