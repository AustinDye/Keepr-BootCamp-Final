<template>
  <div class="component">
    <span class="d-flex">
      <img class="m-2" :src="profile.picture" />
      <div>
        <h1>{{ keeps.length }}</h1>
        <h1>{{ vaults.length }}</h1>
        <h5>3</h5>
      </div>
    </span>
    <div class="btn btn-info" @click="openModal">+</div>
    <div class="keep-container">
      <Vault v-for="v in vaults.slice(0, 20)" :key="v.id" :vault="v" />
    </div>
    <div class="btn btn-info" @click="openKeepModal">+</div>
    <div class="keep-container">
      <Keep v-for="k in keeps.slice(0, 20)" :key="k.id" :keep="k" />
    </div>
  </div>
  <VaultModal />
  <CreateKeepModal />
  <KeepModal />
</template>

<script>
import { computed, onMounted } from "@vue/runtime-core";
import { profilesService } from "../services/ProfileService";
import { useRoute } from "vue-router";
import { AppState } from "../AppState";
import { keepsService } from "../services/KeepsService";
import { vaultsService } from "../services/VaultsService";
import { router } from "../router";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { Modal } from "bootstrap";
export default {
  name: "Profile",
  setup() {
    const route = useRoute();
    onMounted(async () => {
      await profilesService.getById(route.params.id);
      await vaultsService.getByProfile(route.params.id);
      await keepsService.getByProfile(route.params.id);
    });
    return {
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      focusKeep: computed(() => AppState.focusKeep),

      openModal() {
        Modal.getOrCreateInstance(
          document.getElementById("vaultModal")
        ).toggle();
      },
      openKeepModal() {
        Modal.getOrCreateInstance(
          document.getElementById("createKeepModal")
        ).toggle();
      },
    };
  },
};
</script>

<style lang="scss" scoped></style>
