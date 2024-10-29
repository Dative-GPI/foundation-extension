<template>
  <FEDialogCalendarTwin />
</template>

<script lang="ts">
import { defineComponent } from "vue";

import FEDialogCalendarTwin from "@dative-gpi/foundation-extension-shared-ui/components/FEDialogCalendarTwin.vue";

export default defineComponent({
  name: "CalendarTwin",
  components: {
    FEDialogCalendarTwin,
  }
});
</script>