<template>
  <div>
    <FSButton
      v-bind="$attrs"
      class="d-btn-file grey-3 white-2--text"
      @click="onClick"
      color="primary"
    > </FSButton>
    <form>
      <input
        v-show="false"
        type="file"
        ref="input"
        :accept="accept"
        @input="onInput"
      />
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";

export default defineComponent({
  name: "ButtonFile",
  props: {
    accept: {
      type: String,
      required: false,
      default: "",
    },
    readFile: {
      type: Boolean,
      required: false,
      default: true,
    },
  },
  emits: ["input"],
  setup(props, { emit }) {
    const input = ref<HTMLInputElement>();
    const onClick = () => {
      input.value?.click();
    };
    const onInput = () => {
      const file = input.value!.files && input.value!.files[0];
      if (!file) {
        return;
      }
      if (!props.readFile) {
        emit("input", file);
        clear();
        return;
      }
      const reader = new FileReader();
      reader.addEventListener("load", (fileEv) => {
        emit("input", fileEv.target && fileEv.target.result);
        clear();
      });
      reader.readAsDataURL(file);
    };
    const clear = () => {
      input.value!.form && input.value!.form.reset();
    };
    return {
      input,
      onClick,
      onInput,
    };
  },
});
</script>

<style scoped></style>
