import { ComposableFactory, ServiceFactory, buildURL } from "@dative-gpi/bones-ui";

import { APPLICATION_TRANSLATIONS_URL, APPLICATION_TRANSLATIONS_WORKBOOK_URL, APPLICATION_TRANSLATION_URL } from "../config";

import type { ApplicationTranslationDTO, ApplicationTranslationsFilter, DownloadApplicationTranslations, UpdateApplicationTranslation, UploadApplicationTranslations } from "../domain";
import { ApplicationTranslation } from "../domain";

const ApplicationTranslationServiceFactory = new ServiceFactory<ApplicationTranslation, ApplicationTranslationDTO>("extensionApplicationTranslation", ApplicationTranslation)
    .create(f => f.build(
        f.addGetMany<ApplicationTranslationDTO, ApplicationTranslation, ApplicationTranslationsFilter>(APPLICATION_TRANSLATIONS_URL, ApplicationTranslation),
        f.addNotify(notifier => ({
            upsert: async (code: string, payload: UpdateApplicationTranslation) => {
                const response = await ServiceFactory.http.post(APPLICATION_TRANSLATION_URL(code), payload);
                const dtos: ApplicationTranslationDTO[] = response.data;
                const results = dtos.map(d => new ApplicationTranslation(d));

                for (const result of results) {
                    notifier.notify("update", result);
                }

                return results;
            },
            upload: async (payload: UploadApplicationTranslations) => {
                const data = new FormData();
                data.append('file', payload.file);
                for (let i = 0; i < payload.languages.length; i++) {
                    data.append(`languages[${i}].index`, payload.languages[i].index.toString());
                    data.append(`languages[${i}].languageCode`, payload.languages[i].languageCode);
                }

                const response = await ServiceFactory.http.put(APPLICATION_TRANSLATIONS_WORKBOOK_URL, data, { headers: { 'Content-Type': 'multipart/form-data' } });
                const dtos: ApplicationTranslationDTO[] = response.data;

                const applicationTranslations = dtos.map(d => new ApplicationTranslation(d));

                for (const result of applicationTranslations) {
                    notifier.notify("update", result);
                }

                return applicationTranslations;
            },
            download: async (payload: DownloadApplicationTranslations) => {
                const response = await ServiceFactory.http.get(buildURL(APPLICATION_TRANSLATIONS_WORKBOOK_URL, payload), { responseType: 'blob' });

                const file = new File([response.data], payload.fileName, { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });

                return file;
            },
            // notifyRemove: (id: string) => notifier.notify("delete", id)
        }))
    ));

export const useApplicationTranslations = ComposableFactory.getMany(ApplicationTranslationServiceFactory);
export const useUpsertApplicationTranslation = ComposableFactory.custom(ApplicationTranslationServiceFactory.upsert);
export const useUploadApplicationTranslation = ComposableFactory.custom(ApplicationTranslationServiceFactory.upload);
export const useDownloadApplicationTranslation = ComposableFactory.custom(ApplicationTranslationServiceFactory.download);
   
