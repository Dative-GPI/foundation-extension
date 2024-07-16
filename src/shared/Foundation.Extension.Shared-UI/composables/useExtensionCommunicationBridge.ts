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
    if (_height == height) {return}
    _height = height;
    notifyDebounced({
      height: height,
      path: path,
    });
  }

  const setDialogWidth = (dialogWidth: number, path: string) => {
    notify({
      dialogWidth,
      path,
    });
  }
  const setDialogHeight = (dialogHeight: number, path: string) => {
    notify({
      dialogHeight,
      path,
    });
  }

  const openDialog = (path: string) => {
    notify({
      path,
      dialog: true,
    });
  }

  const closeDialog = (path: string, success: boolean = false) => {
    notify({
      path,
      success,
      drawer: false,
    });
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
    notify,
    setTitle,
    setCrumbs,
    setHeight,
    openDialog,
    openDrawer,
    closeDrawer,
    closeDialog,
    setDialogWidth,
    setDialogHeight
  }
}
