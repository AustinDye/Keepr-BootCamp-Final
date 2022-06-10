<template>
  <div class="component m-3">
    <span class="d-flex">
      <img class="m-3" :src="profile.picture" />
      <div>
        <h1>{{ profile.name }}</h1>
        <h3>Keeps: {{ keeps.length }}</h3>
        <h3>Vaults: {{ vaults.length }}</h3>
      </div>
    </span>
    <div class="row d-block">
      <div class="col-md-12">
        <span class="d-flex"
          ><p class="fs-1">Vaults</p>
          <div class="btn fs-1 p-0 text-danger" @click="openModal">+</div></span
        >

        <div class="row d-flex">
          <Vault v-for="v in vaults.slice(0, 20)" :key="v.id" :vault="v" />
        </div>
      </div>
      <div class="col-md-12 mx-2">
        <span class="d-flex"
          ><p class="fs-1">Keeps</p>
          <div class="btn fs-1 p-0 text-danger" @click="openKeepModal">
            +
          </div></span
        >

        <div class="keep-container">
          <Keep v-for="k in keeps" :key="k.id" :keep="k" />
        </div>
      </div>
    </div>
  </div>
  <VaultModal />
  <CreateKeepModal />
  <KeepModal />
</template>

<script>
import { computed, onMounted } from "@vue/runtime-core";
import { profilesService } from "../services/ProfileService";
import { useRoute, useRouter } from "vue-router";
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
    const router = useRouter();
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
