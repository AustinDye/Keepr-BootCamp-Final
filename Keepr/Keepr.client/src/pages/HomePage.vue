<template>
  <div class="keep-container">
    <Keep v-for="k in keeps" :key="k.id" :keep="k" />
  </div>
  <KeepModal />
</template>

<script>
import { computed, onMounted } from "@vue/runtime-core";
import { keepsService } from "../services/KeepsService";
import { AppState } from "../AppState";
import Pop from "../utils/Pop";
import { Modal } from "bootstrap";
import { vaultsService } from "../services/VaultsService";
export default {
  name: "Home",
  setup() {
    onMounted(async () => {
      try {
        await vaultsService.getByProfile(AppState.account.id);
        await keepsService.getAll();
      } catch (error) {
        Pop.toast("Something went wrong", error);
      }
    });
    return {
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      account: computed(() => AppState.account),
    };
  },
};
</script>

<style scoped lang="scss"></style>
