<template>
  <div>
    <div class="btn btn-danger" @click="deleteVault">Delete Vault</div>
    <div class="keep-container">
      <Keep v-for="k in keeps.slice(0, 20)" :key="k.id" :keep="k" />
    </div>
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
import { useRoute, useRouter } from "vue-router";
import { vaultKeepsService } from "../services/VaultKeepsService";
import { router } from "../router";

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
    const router = useRouter();
    const route = useRoute();

    onMounted(async () => {
      try {
        await vaultsService.getById(route.params);
        await keepsService.getByVault(route.params.id);
      } catch (error) {
        Pop.toast("Something went wrong", error);
        router.push({
          name: "Home",
        });
      }
    });

    return {
      keeps: computed(() => AppState.keeps),
      focusVault: computed(() => AppState.focusVault),

      async deleteVault() {
        try {
          if (await Pop.confirm()) {
            router.push({
              name: "Profile",
              params: { id: AppState.focusVault.creatorId },
            });
            await vaultsService.delete(AppState.focusVault);

            Pop.toast("Keep Deleted", "success");
          }
        } catch (error) {
          console.error("[error prefix]", error.message);
          Pop.toast(error.message, "error");
        }
      },
    };
  },
};
</script>

<style scoped lang="scss"></style>
