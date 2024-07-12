import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

import { TABLES_URL, TABLE_PROPERTIES_URL, TABLE_URL } from "../config";

import type { TableDetailsDTO} from "../domain";
import { TableDetails, TableInfos } from "../domain";


const TableServiceFactory = new ServiceFactory<TableDetails, TableDetailsDTO>("disposition", TableDetails)
    .create(f => f.build(
        f.addGet(TABLE_URL),
        f.addGetMany(TABLES_URL, TableInfos),
        f.addUpdate(TABLE_URL),
        f.addNotify(notifier => ({
            synchronizeColumns: async (tableId: string) => {
                const response = await ServiceFactory.http.patch(TABLE_PROPERTIES_URL(tableId));
                const dto: TableDetailsDTO = response.data;
                const result = new TableDetails(dto);
                
                notifier.notify("update", result);
            }
        }))));


export const useTable = ComposableFactory.get(TableServiceFactory);
export const useTables = ComposableFactory.getMany(TableServiceFactory);
export const useUpdateTable = ComposableFactory.update(TableServiceFactory);
export const useSynchronizeTable = ComposableFactory.custom(TableServiceFactory.synchronizeColumns);