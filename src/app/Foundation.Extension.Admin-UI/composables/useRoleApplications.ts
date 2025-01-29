import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

import { RoleApplicationDetails, type RoleApplicationDetailsDTO, type UpdateRoleApplicationDTO } from "../domain";
import { ROLE_APPLICATION_URL } from "../config";

const RoleApplicationServiceFactory = new ServiceFactory<RoleApplicationDetailsDTO, RoleApplicationDetails>("roleApplications", RoleApplicationDetails).create(factory => factory.build(
  factory.addGet(ROLE_APPLICATION_URL),
  factory.addUpdate<UpdateRoleApplicationDTO>(ROLE_APPLICATION_URL),
  factory.addNotify()
));

export const useRoleApplication = ComposableFactory.get(RoleApplicationServiceFactory);
export const useUpdateRoleApplication = ComposableFactory.update(RoleApplicationServiceFactory);