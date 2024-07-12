export class Translation {
  id: string;
  code: string;
  value: string;

  constructor(params: TranslationDTO) {
    this.id = params.id;
    this.code = params.code;
    this.value = params.value;
  }
}

export interface TranslationDTO {
  id: string;
  code: string;
  value: string;
}

export interface TranslationFilter {
  prefix?: string;
}