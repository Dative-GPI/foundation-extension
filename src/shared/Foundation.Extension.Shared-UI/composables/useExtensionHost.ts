import { onMounted, onUnmounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';

import { useAppAuthToken, useAppLanguageCode, useAppTimeZone } from "@dative-gpi/foundation-shared-services/composables";

import { useExtensionCommunicationBridge } from './useExtensionCommunicationBridge';

const done = ref(false);

const languageCode = ref<string | null>(null);
const timeZone = ref<string | null>(null);
const token = ref<string | null>(null);

export const useExtensionHost = () => {
  onMounted(() => {
    if (done.value) {
      return;
    }

    languageCode.value = new URL(window.location.toString()).searchParams.get("languageCode");
    timeZone.value = new URL(window.location.toString()).searchParams.get("timeZone");
    token.value = new URL(window.location.toString()).searchParams.get("token");

    const { goTo, setHeight } = useExtensionCommunicationBridge();
    const { setAppLanguageCode } = useAppLanguageCode();
    const { setAppAuthToken } = useAppAuthToken();
    const { setAppTimeZone } = useAppTimeZone();
    const router = useRouter();
    const route = useRoute();

    const unsubscribeRouterHook = router.afterEach((to, from) => {
      // inital route, no need to notify the host about the change
      if (!from || !from.name) {
        return;
      }

      // embedded route, no need to notify the host about the change
      if (to.meta && to.meta.overlay) {
        return;
      }

      goTo(to.path);
    });

    const intervalId = setInterval(() => {
      setHeight(document.body.scrollHeight, route.path);
    }, 10)

    if (languageCode.value) {
      setAppLanguageCode(languageCode.value);
    }
    if (timeZone.value) {
      setAppTimeZone(timeZone.value);
    }
    if (token.value) {
      setAppAuthToken(token.value);
    }

    onUnmounted(() => {
      unsubscribeRouterHook();
      clearInterval(intervalId);
    });

    done.value = true;
  });

  return {
    languageCode,
    timeZone,
    token,
    done
  };
}
