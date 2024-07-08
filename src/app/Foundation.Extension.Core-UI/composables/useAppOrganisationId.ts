import { computed, ref } from "vue";
import { setOrganisationId } from "../config/urls/urlFactory";

const organisationId = ref<string | null>(null);
const initialized = computed(() => (organisationId.value != null));

export const useAppOrganisationId = () => {
    const setAppOrganisationId = (payload: string) => {
        organisationId.value = payload;
        setOrganisationId(organisationId);
    };

    return {
        initialized,
        organisationId,
        setAppOrganisationId
    }
}