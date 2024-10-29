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

export interface DateRangePayload {
  messageType: "dialog-date-range";
  dialogId: string;
  dateRange: number[] | null;
  title: string | null;
}

export const dateRangePayloadSchema: JTDSchemaType<DateRangePayload> = {
  properties: {
    messageType: { enum: ["dialog-date-range"] },
    dialogId: { type: "string" },
    dateRange: { elements: {
      type: "int32"
    }, nullable: true },
    title: { type: "string", nullable: true }
  }
};

export interface SubmitDateRange {
  messageType: "dialog-submit-date-range";
  dialogId: string;
  dateRange: number[] | null;
}

export const submitDateRangeSchema: JTDSchemaType<SubmitDateRange> = {
  properties: {
    messageType: { enum: ["dialog-submit-date-range"] },
    dialogId: { type: "string" },
    dateRange: { elements: {
      type: "int32"
    }, nullable: true }
  }
};




