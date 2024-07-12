export class ApplicationTranslation {
  id: string;
  code: string;
  value: string;

  constructor(params: ApplicationTranslationDTO) {
    this.id = params.id;
    this.code = params.code;
    this.value = params.value;
  }
}

export interface ApplicationTranslationDTO {
  id: string;
  code: string;
  value: string;
}