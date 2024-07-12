import { Ref, ref } from "vue";

const organisationId = ref<string | null>(null);

export function setOrganisationId(orgId: Ref<string | null>) {
    organisationId.value = orgId.value;
}

export function urlFactory(url: (orgId: string) => string) {
    return () => {
        if (!organisationId.value)
            throw new Error("OrganisationId is not set");

        return url(organisationId.value);
    }
}