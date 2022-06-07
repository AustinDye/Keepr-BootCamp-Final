<template>
  <div class="component">
    <span class="d-flex">
      <img :src="profile.picture" />
      <div>
        <h1>{{ keeps.length }}</h1>
        <h1>{{ vaults.length }}</h1>
        <h5></h5>
      </div>
    </span>
  </div>
</template>

<script>
import { computed, onMounted } from "@vue/runtime-core";
import { profilesService } from "../services/ProfileService";
import { useRoute } from "vue-router";
import { AppState } from "../AppState";
import { keepsService } from "../services/KeepsService";
import { vaultsService } from "../services/VaultsService";
import { router } from "../router";
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
    };
  },
};
</script>

<style lang="scss" scoped></style>
