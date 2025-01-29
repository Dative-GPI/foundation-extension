import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

import { ORGANISATION_TYPE_TABLE_URL } from "../config";

import type { OrganisationTypeTableDetailsDTO, UpdateOrganisationTypeTableDTO } from "../domain";
import { OrganisationTypeTableDetails } from "../domain";

import axios from "axios";

const OrganisationTypeTableServiceFactory = new ServiceFactory<OrganisationTypeTableDetails, OrganisationTypeTableDetailsDTO>("extensionOrganisationTypeTable", OrganisationTypeTableDetails)
    .create(f => f.build(
        f.addNotify(
            notifier => ({
                get: async (organisationTypeId: string, tableId: string) => {
                    const response = await axios.get(ORGANISATION_TYPE_TABLE_URL(organisationTypeId, tableId));
                    const data: OrganisationTypeTableDetailsDTO = response.data;
                    return new OrganisationTypeTableDetails(data);
                },
                update: async (organisationTypeId: string, tableId: string, payload: UpdateOrganisationTypeTableDTO) => {
                    const response = await axios.post(ORGANISATION_TYPE_TABLE_URL(organisationTypeId, tableId), payload);
                    const data: OrganisationTypeTableDetailsDTO = response.data;
                    const result = new OrganisationTypeTableDetails(data);

                    notifier.notify("update", result);

                    return result;
                }
            })
        )
    ));


export const useOrganisationTypeTable = ComposableFactory.custom(OrganisationTypeTableServiceFactory.get);
export const useUpdateOrganisationTypeTable = ComposableFactory.custom(OrganisationTypeTableServiceFactory.update);