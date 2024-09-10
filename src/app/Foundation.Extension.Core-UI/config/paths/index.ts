import { urlFactory } from "@dative-gpi/foundation-core-services/config/urls/urlFactory";

export const CORE_PATH = urlFactory(organisationId => `/organisations/${encodeURIComponent(organisationId)}`);

export * from "./dialogs";