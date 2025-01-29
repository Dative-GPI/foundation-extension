import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

import { TRANSLATIONS_URL } from "../config";

import type { TranslationDTO, TranslationFilter } from "../domain";
import { Translation } from "../domain";


const TranslationServiceFactory = new ServiceFactory<Translation, TranslationDTO>("extensionTranslation", Translation).create(factory => factory.build(
  factory.addGetMany<TranslationDTO, Translation, TranslationFilter>(TRANSLATIONS_URL, Translation),
  factory.addNotify()
));

export const useTranslations = ComposableFactory.getMany(TranslationServiceFactory);