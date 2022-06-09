<template>
  <div class="col-md-3">
    <div class="vbox">
      <img
        class="vault-box rounded"
        :src="vault.img"
        alt=""
        @click="goToVault"
      />
      <span>
        <h5 class="text-light vault-text">{{ vault.name }}</h5>
      </span>
    </div>
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
      account: computed(() => AppState.account),

      async goToVault() {
        if (
          props.vault.isPrivate &&
          props.vault.creatorId != AppState.account.id
        ) {
          Pop.toast("You can't view that Vault!");
          router.push({ name: "Home" });
        }
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

.vault-box {
  width: 15em;
  height: 15em;
  box-shadow: 0.5em 0.5em 0.5em rgba(0, 0, 0, 0.345);
}

.vault-text {
  position: relative;
  bottom: 1.5em;
}
</style>
