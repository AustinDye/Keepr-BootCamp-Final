<template>
  <figure>
    <img class="img-fluid" :src="keep.img" alt="" @click="getKeep" />
    <figcaption class="d-flex justify-content-between">
      <h1 class="text-light">{{ keep.name }}</h1>

      <img
        @click="goToProfile"
        class="user-img rounded-circle"
        :src="keep.creator.picture"
      />
    </figcaption>
  </figure>
</template>

<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";
import { keepsService } from "../services/KeepsService";
import { Modal } from "bootstrap";
import Pop from "../utils/Pop";
import { router } from "../router";
import { useRouter } from "vue-router";
import { vaultsService } from "../services/VaultsService";
export default {
  props: {
    keep: {
      type: Object,
    },
  },
  setup(props) {
    const router = useRouter();
    return {
      keeps: computed(() => AppState.keeps),
      focusKeep: computed(() => AppState.focusKeep),

      async getKeep() {
        try {
          AppState.focusKeep = props.keep;
          AppState.focusKeep.views++;
          await keepsService.getById(AppState.focusKeep);
          Modal.getOrCreateInstance(
            document.getElementById("keepModal")
          ).toggle();
          await vaultsService.getByProfile(AppState.account.id);
        } catch {
          Pop.toast("You cant view that keep!");
        }
      },
      async goToProfile() {
        router.push({ name: "Profile", params: { id: props.keep.creatorId } });
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
