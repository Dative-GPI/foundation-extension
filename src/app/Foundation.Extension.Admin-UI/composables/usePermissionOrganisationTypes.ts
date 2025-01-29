import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

import { PERMISSION_ORGANISATION_TYPES_URL } from "../config";

import type { PermissionOrganisationTypeInfosDTO, UpsertPermissionOrganisation, PermissionOrganisationTypesFilter } from "../domain";
import { PermissionOrganisationTypeInfos } from "../domain";

const PermissionServiceFactory = new ServiceFactory<PermissionOrganisationTypeInfosDTO, PermissionOrganisationTypeInfos>("extensionPermissionOrganisationType", PermissionOrganisationTypeInfos)
    .create(f => f.build(
        f.addGetMany<PermissionOrganisationTypeInfosDTO, PermissionOrganisationTypeInfos, PermissionOrganisationTypesFilter>(PERMISSION_ORGANISATION_TYPES_URL, PermissionOrganisationTypeInfos),
        f.addNotify(
            notifier => ({
                upsert: async (payload: UpsertPermissionOrganisation[]) => {
                    const response = await ServiceFactory.http.patch(PERMISSION_ORGANISATION_TYPES_URL, payload);
                    const dtos: PermissionOrganisationTypeInfosDTO[] = response.data;
                    const results = dtos.map(d => new PermissionOrganisationTypeInfos(d));

                    for (const result of results) {
                        notifier.notify("update", result);
                    }

                    return results;
                }
            })
        )
    ));

export const usePermissionOrganisationTypes = ComposableFactory.getMany(PermissionServiceFactory);
export const useUpsertPermissionOrganisationTypes = ComposableFactory.custom(PermissionServiceFactory.upsert);