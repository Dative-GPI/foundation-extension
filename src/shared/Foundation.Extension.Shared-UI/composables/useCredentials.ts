import { Ref, inject } from "vue";
import { ACCESS_TOKEN } from "../config";

export function useCredentials() {
    const token = inject<Ref<string>>(ACCESS_TOKEN);

    if (!token || !token.value) throw new Error("No token found");

    return {
        token
    }
}