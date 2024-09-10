import type { JTDSchemaType } from "ajv/dist/jtd";

export interface DialogReady {
  messageType: "dialog-ready";
  dialogId: string;
}

export const dialogReadySchema: JTDSchemaType<DialogReady> = {
  properties: {
    messageType: { enum: ["dialog-ready"] },
    dialogId: { type: "string" }
  }
};

export interface DialogSubmit {
  messageType: "dialog-submit";
  dialogId: string;
}

export const dialogSubmitSchema: JTDSchemaType<DialogSubmit> = {
  properties: {
    messageType: { enum: ["dialog-submit"] },
    dialogId: { type: "string" }  
  }
};

export interface RemovePayload {
  messageType: "dialog-remove";
  dialogId: string;
  removeCurrent: number;
  removeTotal: number;
}

export const removePayloadSchema: JTDSchemaType<RemovePayload> = {
  properties: {
    messageType: { enum: ["dialog-remove"] },
    dialogId: { type: "string" },
    removeCurrent: { type: "int32" },
    removeTotal: { type: "int32" }
  }
};




