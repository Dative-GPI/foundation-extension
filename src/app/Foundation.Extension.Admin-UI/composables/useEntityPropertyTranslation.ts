import { ComposableFactory, buildURL, ServiceFactory } from "@dative-gpi/bones-ui";

import { ENTITYPROPERTY_TRANSLATIONS_URL, ENTITYPROPERTY_TRANSLATION_URL, ENTITYPROPERTY_TRANSLATIONS_WORKBOOK_URL } from "../config";

import type { EntityPropertyTranslationDTO, DownloadEntityPropertyTranslations, UpdateEntityPropertyTranslation, UploadEntityPropertyTranslations } from "../domain";
import { EntityPropertyTranslationInfos } from "../domain";

const EntityPropertyTranslationServiceFactory = new ServiceFactory<EntityPropertyTranslationInfos, EntityPropertyTranslationDTO>("extensionEntityPropertyTranslation", EntityPropertyTranslationInfos)
    .create(f => f.build(
        f.addGetMany(ENTITYPROPERTY_TRANSLATIONS_URL, EntityPropertyTranslationInfos),
        f.addUpdate<UpdateEntityPropertyTranslation>(ENTITYPROPERTY_TRANSLATION_URL),
        f.addNotify(() => ({
            upload: async (payload: UploadEntityPropertyTranslations) => {
                const data = new FormData();
                data.append('file', payload.file);

                for (let i = 0; i < payload.languages.length; i++) {
                    data.append(`languages[${i}].index`, payload.languages[i].index.toString());
                    data.append(`languages[${i}].languageCode`, payload.languages[i].languageCode);
                }

                const response = await ServiceFactory.http.put(ENTITYPROPERTY_TRANSLATIONS_WORKBOOK_URL, data, { headers: { 'Content-Type': 'multipart/form-data' } });
                const dtos: EntityPropertyTranslationDTO[] = response.data;

                const entityPropertyTranslations = dtos.map(d => new EntityPropertyTranslationInfos(d));

                return entityPropertyTranslations;
            },
            download: async (payload: DownloadEntityPropertyTranslations) => {
                const response = await ServiceFactory.http.get(buildURL(ENTITYPROPERTY_TRANSLATIONS_WORKBOOK_URL, payload), { responseType: 'blob' });

                const file = new File([response.data], payload.fileName, { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });

                return file;
            },
        }))
    ));


export const useEntityProtertyTranslations = ComposableFactory.getMany(EntityPropertyTranslationServiceFactory);
export const useUpdateEntityPropertyTranslation = ComposableFactory.update(EntityPropertyTranslationServiceFactory);
export const useUploadEntityPropertyTranslation = ComposableFactory.custom(EntityPropertyTranslationServiceFactory.upload);
export const useDownloadEntityPropertyTranslation = ComposableFactory.custom(EntityPropertyTranslationServiceFactory.download);