import { onMounted, ref } from "vue";
import type { JTDParser, JTDSchemaType } from "ajv/dist/jtd";
import Ajv from "ajv/dist/jtd";
import _ from "lodash";

let _height = 0;

export function useExtensionCommunicationBridge() {
  const subscribers = ref<Subscriber[]>([]);
  const unsafeSubscribers = ref<UnsafeSubscriber[]>([]);
  const counter = ref(0);
  const ajv = new Ajv();

  const notify = (payload: any) => {
    if (window.top) {
      window.top.postMessage(JSON.stringify(payload), "*");
    }
  }

  const subscribe = <T> (
    schema: JTDSchemaType<T>, 
    uri: string, 
    callback: (payload: T) => void
  ): number => {
    counter.value++;
    subscribers.value.push({
      parser: ajv.compileParser(schema),
      uri,
      callback,
      id: counter.value,
    });
    return counter.value;
  }

  const subscribeUnsafe = <T> (
    uri: string, 
    callback: (payload: T) => void,
    valid: (payload: T) => boolean 
  ): number => {
    counter.value++;
    unsafeSubscribers.value.push({
      uri,
      callback,
      valid,
      id: counter.value,
    });
    return counter.value;
  }
    
  const unsubscribe = (id: number) => {
    const index = subscribers.value.findIndex((s) => s.id == id);
    if (index != -1) {
      subscribers.value.splice(index, 1);
    }
    const unsafeIndex = unsafeSubscribers.value.findIndex((s) => s.id == id);
    if (unsafeIndex != -1) {
      unsafeSubscribers.value.splice(unsafeIndex, 1);
    }
  }

  const onMessageReceived = (event: MessageEvent) => {
    try {
      JSON.parse(event.data);
    } catch {
      return;
    }

    subscribers.value
      // .filter((s) => s.uri === "*" || new URL(event.origin).hostname == new URL(s.uri).hostname)
      .map((s) => ({
        callback: s.callback,
        url: new URL(s.uri),
        data: s.parser(event.data),
      }))
      .filter((s) => !!s.data)
      .forEach((s) => {
        try {
          s.callback(s.data);
        } catch (error) {
          console.log(error);
        }
      });

    unsafeSubscribers.value
      .map((s) => ({
        callback: s.callback,
        url: new URL(s.uri),
        data: JSON.parse(event.data),
        valid: s.valid,
      }))
      .filter((s) => {
        return s.valid(s.data);
      })
      .forEach((s) => {
        try {
          s.callback(s.data);
        } catch (error) {
          console.log(error);
        }
      });
  }

  const notifyDebounced = _.debounce(notify, 50);

  const goTo = (path: string) => {
    notify({
      path: path,
    });
  }

  const setHeight = (height: number, path: string) => {
    if (_height == height) {return}
    _height = height;
    notifyDebounced({
      height: height,
      path: path,
    });
  }

  const setDialogWidth = (dialogWidth: string[] | number[] | string | number, path: string) => {
    notify({
      dialogWidth: JSON.stringify(dialogWidth),
      path
    });
  }
  const setDialogHeight = (dialogHeight: string[] | number[] | string | number, path: string) => {
    notify({
      dialogHeight: JSON.stringify(dialogHeight),
      path
    });
  }

  const openDialog = (path: string) => {
    notify({
      path,
      dialog: true
    });
  }

  const closeDialog = (path: string, success: boolean = false) => {
    notify({
      path,
      success,
      dialog: false
    });
  }

  const setDialogMounted = (path: string, dialogMounted: boolean = true) => {
    notify({
      path,
      dialogMounted
    });
  }

  //Deprecated
  const openDrawer = (path: string) => {
    notify({
      path,
      drawer: true,
    });
  }
  
  //Deprecated
  const closeDrawer = (path: string, success: boolean = false) => {
    notify({
      path,
      success,
      drawer: false,
    });
  }

  onMounted(() => {
    window.addEventListener(
      "message",
      onMessageReceived,
      false
    );
  });

  return {
    goTo,
    notify,
    subscribe,
    setHeight,
    openDialog,
    openDrawer,
    closeDrawer,
    closeDialog,
    unsubscribe,
    setDialogWidth,
    setDialogHeight,
    subscribeUnsafe,
    setDialogMounted
  }
}


interface Subscriber {
  parser: JTDParser;
  uri: string;
  callback: (payload: any) => void;
  id: number;
}

interface UnsafeSubscriber {
  uri: string;
  callback: (payload: any) => void;
  id: number;
  valid: (payload: any) => boolean;
}
