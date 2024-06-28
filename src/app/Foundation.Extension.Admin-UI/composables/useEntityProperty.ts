import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

import { ENTITYPROPERTIES_URL } from "../config";

import { EntityPropertyInfos, EntityPropertyDTO } from "../domain";


const EntityPropertyServiceFactory = new ServiceFactory<EntityPropertyInfos, EntityPropertyDTO>("entity-property", EntityPropertyInfos)
    .create(f => f.build(
        f.addGetMany(ENTITYPROPERTIES_URL, EntityPropertyInfos),
        f.addNotify()
    ));


export const useEntityProterties = ComposableFactory.getMany(EntityPropertyServiceFactory);