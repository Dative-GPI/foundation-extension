export {};

declare module "@vue/runtime-core" {
  interface ComponentCustomProperties {
    $tr: (code: string, defaultLabel: string, ...parameters: (string | number)[]) => string;
    $pm: {
      some(...permissionCodes: string[]): boolean;
      every(...permissionCodes: string[]): boolean;
    };
  }
}

declare module "vue" {
  interface ComponentCustomProperties {
    $tr: (code: string, defaultLabel: string, ...parameters: (string | number)[]) => string;
    $pm: {
      some(...permissionCodes: string[]): boolean;
      every(...permissionCodes: string[]): boolean;
    };
  }
}

declare global {
  interface Window {
    _bonesQueue: any;
  }
}