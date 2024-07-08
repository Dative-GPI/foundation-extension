import { computed, ref } from "vue";

const token = ref<string | undefined>(undefined);

export const useAppToken = () => {
    const setAppToken = (payload: string) => {
        token.value = payload;
    };

    const ready = computed(() => token.value !== null);

    return {
        ready,
        token,
        setAppToken
    };
}