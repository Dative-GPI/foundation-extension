import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

import { Language, type LanguageDTO } from "../domain";
import { APPLICATION_LANGUAGES_URL } from "../config";

const ApplicationLanguageServiceFactory = new ServiceFactory<LanguageDTO, Language>("extensionApplicationLanguage", Language).create(f => f.build(
  f.addGetMany<LanguageDTO, Language, {}>(APPLICATION_LANGUAGES_URL, Language),
  f.addNotify()
));

export const useApplicationLanguages = ComposableFactory.getMany(ApplicationLanguageServiceFactory);