<template>
  <div class="">
    <figure class="selectable">
      <img class="" :src="vault.img" alt="" @click="goToVault" />
      <figcaption class="">
        <h1 class="text-light">{{ vault.name }}</h1>
      </figcaption>
    </figure>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";
import { vaultsService } from "../services/VaultsService";
import { Modal } from "bootstrap";
import Pop from "../utils/Pop";
import { router } from "../router";
import { useRouter } from "vue-router";
export default {
  props: {
    vault: {
      type: Object,
    },
  },
  setup(props) {
    return {
      vaults: computed(() => AppState.vaults),

      async goToVault() {
        console.log(props.vault);
        AppState.focusVault = props.vault;
        router.push({ name: "Vault", params: { id: props.vault.id } });
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
