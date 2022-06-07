<template>
  <div>
    <figure class="selectable" @click="getKeep()">
      <img class="" :src="keep.img" alt="" />
      <figcaption class="">
        <h1 class="text-light">{{ keep.name }}</h1>
        <a
          ><img class="user-img rounded-circle" :src="keep.creator.picture"
        /></a>
      </figcaption>
    </figure>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";
import { keepsService } from "../services/KeepsService";
import { Modal } from "bootstrap";
import Pop from "../utils/Pop";
export default {
  props: {
    keep: {
      type: Object,
    },
  },
  setup(props) {
    return {
      keeps: computed(() => AppState.keeps),
      focusKeep: computed(() => AppState.focusKeep),

      async getKeep() {
        try {
          AppState.focusKeep = props.keep;
          console.log(AppState.focusKeep);
          Modal.getOrCreateInstance(
            document.getElementById("keepModal")
          ).toggle();
        } catch {
          Pop.toast("You cant view that keep!");
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.user-img {
  width: 2em;
  height: 2em;
}

.line {
  height: 0.1em;
  width: 80%;
  background-color: rgba(0, 0, 0, 0.488);
}
</style>
