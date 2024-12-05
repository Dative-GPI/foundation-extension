import { onMounted, onUnmounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';

import { useAppAuthToken, useAppLanguageCode, useAppTimeZone, useAppLanguages, useLanguages, useAppHost } from "@dative-gpi/foundation-shared-services/composables";

import { useExtensionCommunicationBridge } from './useExtensionCommunicationBridge';

const done = ref(false);

const languageCode = ref<string | null>(null);
const timeZone = ref<string | null>(null);
const token = ref<string | null>(null);
const host = ref<string | null>(null);

export const useExtensionHost = () => {
  let unsubscribeRouterHook: () => void;
  let intervalId: NodeJS.Timeout;

  onMounted(async () => {
    if (done.value) {
      return;
    }

    languageCode.value = new URL(window.location.toString()).searchParams.get("languageCode");
    timeZone.value = new URL(window.location.toString()).searchParams.get("timeZone");
    token.value = new URL(window.location.toString()).searchParams.get("token");
    host.value = new URL(window.location.toString()).searchParams.get("host");

    const { goTo, setHeight } = useExtensionCommunicationBridge();
    const { setAppLanguageCode } = useAppLanguageCode();
    const { getMany: getLanguages } = useLanguages();
    const { setAppAuthToken } = useAppAuthToken();
    const { setAppLanguages } = useAppLanguages();
    const { setAppTimeZone } = useAppTimeZone();
    const { setAppHost } = useAppHost();
    const router = useRouter();
    const route = useRoute();

    unsubscribeRouterHook = router.afterEach((to, from) => {
      if (!from || !from.name) {
        return;
      }
      if (to.meta && to.meta.overlay) {
        return;
      }
      goTo(to.path);
    });

    intervalId = setInterval(() => {
      setHeight(document.body.scrollHeight, route.path);
    }, 10);

    if (languageCode.value) {
      setAppLanguageCode(languageCode.value);
    }
    if (timeZone.value) {
      setAppTimeZone(timeZone.value);
    }
    if (token.value) {
      setAppAuthToken(token.value);
    }
    if (host.value) {
      setAppHost(decodeURIComponent(host.value));
    }

    const languages = await getLanguages();
    if (languages.value) {
      setAppLanguages(languages.value);
    }

    done.value = true;
  });

  onUnmounted(() => {
    if (unsubscribeRouterHook) {
      unsubscribeRouterHook();
    }
    if (intervalId) {
      clearInterval(intervalId);
    }
  });

  return {
    languageCode,
    timeZone,
    token,
    done
  };
}
