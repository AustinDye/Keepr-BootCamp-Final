<template>
  <div class="d-flex justify-content-between">
    <h1>1</h1>
    <h6>1</h6>
    <div class="btn btn-info">Delete</div>
  </div>
  <div class="keep-container">
    <Keep v-for="k in keeps.slice(0, 20)" :key="k.id" :keep="k" />
  </div>
</template>

<script>
import { computed, onMounted } from "@vue/runtime-core";
import { keepsService } from "../services/KeepsService";
import { AppState } from "../AppState";
import Pop from "../utils/Pop";
import { Modal } from "bootstrap";
import { vaultsService } from "../services/VaultsService";
import { useRoute } from "vue-router";

export default {
  props: {
    focusVault: {
      type: Object,
    },
  },
  props: {
    keep: {
      type: Object,
    },
  },
  name: "Vault",
  setup() {
    const route = useRoute();
    onMounted(async () => {
      try {
        await vaultsService.getById(route.params);
        await keepsService.getByVault(route.params.id);
      } catch (error) {
        Pop.toast("Something went wrong", error);
      }
    });
    return {
      keeps: computed(() => AppState.keeps),
      focusVault: computed(() => AppState.focusVault),
    };
  },
};
</script>

<style scoped lang="scss"></style>
