import { computed, ref } from "vue";
import { useAppOrganisationId as useSharedOrganisationId } from "@dative-gpi/foundation-core-services/composables";

import { setOrganisationId } from "../config/urls/urlFactory";

const organisationId = ref<string | null>(null);
const initialized = computed(() => (organisationId.value != null));

const { setAppOrganisationId: setSharedOrganisationId } = useSharedOrganisationId();

export const useAppOrganisationId = () => {
    const setAppOrganisationId = (payload: string) => {
        organisationId.value = payload;
        setOrganisationId(organisationId);
        setSharedOrganisationId(organisationId.value);
    };

    return {
        initialized,
        organisationId,
        setAppOrganisationId
    }
}