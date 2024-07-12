import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

import { APPLICATION_LANGUAGES_URL } from "../config";

import type { LanguageDTO } from "../domain";
import { Language } from "../domain";

const ApplicationLanguageServiceFactory = new ServiceFactory<LanguageDTO, Language>("application-language", Language)
    .create(f => f.build(
        f.addNotify(),
        f.addGetMany<LanguageDTO, Language, {}>(APPLICATION_LANGUAGES_URL, Language),
    ));

export const useApplicationLanguages = ComposableFactory.getMany(ApplicationLanguageServiceFactory);


