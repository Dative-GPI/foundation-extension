<template>
  <div
    name="drawer"
    style="height: 100vh"
    class="pa-3"
  >
    <slot
      name="header"
    >
      <FSRow
        no-gutters
        align="start"
        justify="start"
        class="mb-3"
        style="flex-wrap: nowrap"
      >
        <FSButton
          variant="icon"
          icon="mdi-chevron-right"
          @click="close(false)"
          class="align-self-center"
        />

        <slot
          name="title-outer"
        >
          <h2
            class="ml-2"
          >
            <slot
              name="title"
            >
              {{ title }}
            </slot>
          </h2>
        </slot>
      </FSRow>
    </slot>

    <slot></slot>

    <div
      style="height: 40px"
    />
    <v-footer
      fixed
      v-if="$slots['actions']"
      color="transparent"
      style="background-color: white"
    >
      <slot
        name="actions"
      ></slot>
    </v-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent, watch, onMounted } from "vue";

import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui";

export default defineComponent({
  name: "Drawer",
  props: {
    title: {
      type: String,
      required: true
    },
    value: {
      type: Boolean,
      required: true
    },
    width: {
      type: Number,
      required: false,
      default: 600
    }
  },
  setup(props) {
    const extension = useExtensionCommunicationBridge();

    const setWidth = () => {
      extension.setDialogWidth(props.width, location.pathname);
    };

    const close = (success: boolean) => {
      extension.closeDrawer(location.pathname, success);
    };

    onMounted(setWidth);

    watch(
      () => props.width,
      (value) => {
        if (value) {
          setWidth();
        }
      }
    );

    watch(
      () => props.value,
      (value) => {
        if (value) {
          close(true);
        }
      }
    );

    return {
      close,
    };
  }
});
</script>