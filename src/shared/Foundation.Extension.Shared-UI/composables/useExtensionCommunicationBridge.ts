import _ from "lodash"

let _height = 0;

export function useExtensionCommunicationBridge() {

  const notify = (payload: any) => {
    if (window.top) {
      window.top.postMessage(JSON.stringify(payload), "*");
    }
  }

  const notifyDebounced = _.debounce(notify, 50);

  const goTo = (path: string) => {
    notify({
      path: path,
    });
  }

  const setTitle = (title: string) => {
    notify({
      title: title,
    });
  }

  const setCrumbs = (crumbs: any[]) => {
    notify({
      crumbs: crumbs,
    });
  }

  const setHeight = (height: number, path: string) => {
    if (_height == height) return

    _height = height;
    notifyDebounced({
      height: height,
      path: path,
    });
  }

  const setWidth = (width: number, path: string) => {
    notify({
      width,
      path,
    });
  }

  const openDialog = (path: string) => {
    throw new Error("Method not implemented.");
  }

  const closeDialog = (path: string) => {
    throw new Error("Method not implemented.");
  }

  const openDrawer = (path: string) => {
    notify({
      path,
      drawer: true,
    });
  }

  const closeDrawer = (path: string, success: boolean = false) => {
    notify({
      path,
      success,
      drawer: false,
    });
  }

  return {
    goTo,
    setTitle,
    setCrumbs,
    setHeight,
    setWidth,
    openDialog,
    closeDialog,
    openDrawer,
    closeDrawer,
    notify
  }
}
