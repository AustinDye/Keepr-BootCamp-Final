<template>
  <Modal id="keepModal">
    <template #body>
      <div class="row" v-if="focusKeep.creator">
        <div class="col-6">
          <img class="img-fluid" :src="focusKeep.img" alt="" />
        </div>
        <div class="col-6">
          <div class="row d-block">
            <div class="col-12">
              <span class="d-flex justify-content-center m-4 text-center">
                <h5>
                  <i class="mdi mdi-eye mx-2 fs-2"></i>{{ focusKeep.views }}
                </h5>
                <h5>
                  <i class="mdi mdi-alpha-k-box-outline fs-2 mx-2"></i
                  >{{ focusKeep.kept }}
                </h5>
                <h5><i class="mdi mdi-share-variant fs-2 mx-2"></i>15</h5>
              </span>
            </div>
            <div class="col-12">
              <h3 class="text-center">{{ focusKeep.name }}</h3>
              <p>{{ focusKeep.description }}</p>
              <div class="line"></div>
            </div>
            <div class="col-12">
              <div class="row infostuff">
                <div class="col-3 pt-2 d-flex justify-content-center">
                  <button
                    v-if="focusVault.creatorId == account.id && focusVault"
                    class="btn btn-danger"
                    @click="deleteVk"
                  >
                    Remove
                  </button>
                  <div class="dropdown" v-else>
                    <a
                      class="btn btn-secondary dropdown-toggle"
                      href="#"
                      role="button"
                      id="dropdownMenuLink"
                      data-bs-toggle="dropdown"
                      aria-expanded="false"
                    >
                      Add to Vault
                    </a>

                    <ul
                      class="dropdown-menu"
                      aria-labelledby="dropdownMenuLink"
                    >
                      <p
                        v-for="v in vaults"
                        :key="v.id"
                        :vault="v"
                        :value="v.id"
                      >
                        <a class="dropdown-item" @click="createVk(v)">
                          {{ v.name }}</a
                        >
                      </p>
                    </ul>
                  </div>
                </div>
                <div
                  class="col-3 d-flex justify-content-center"
                  v-if="focusKeep.creatorId == account.id"
                >
                  <i class="mdi mdi-trash-can fs-1" @click="deleteKeep"></i>
                </div>
                <div class="col-6 pt-1 ps-4">
                  <div
                    v-if="focusKeep.creator.id"
                    class="rounded d-flex selectable"
                  >
                    <div class="rounded accountbar p-1">
                      <img
                        @click="goToProfile"
                        :src="focusKeep.creator.picture"
                        alt="account photo"
                        height="40"
                        class="rounded"
                      />
                      <span class="mx-3 text- lighten-30">{{
                        focusKeep.creator.name
                      }}</span>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>

<script>
import { computed, ref } from "@vue/reactivity";
import { AppState } from "../AppState";
import { useRoute, useRouter } from "vue-router";
import { vaultKeepsService } from "../services/VaultKeepsService";
import Pop from "../utils/Pop";
import { logger } from "../utils/Logger";
import { onMounted, watchEffect } from "@vue/runtime-core";
import { Modal } from "bootstrap";
import { keepsService } from "../services/KeepsService";
import { vaultsService } from "../services/VaultsService";
export default {
  props: {
    vault: {
      type: Object,
    },
  },
  setup() {
    const editable = ref({ keepId: AppState.focusKeep.id });
    const route = useRoute();
    const router = useRouter();

    return {
      editable,
      focusKeep: computed(() => AppState.focusKeep),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      focusVault: computed(() => AppState.focusVault),

      async createVk(v) {
        try {
          editable.vaultId = v.id;
          logger.log(editable);
          const vk = await vaultKeepsService.createVk(editable);
          Pop.toast("Keep added to " + v.name);
        } catch (error) {
          console.error("[error prefix]", error.message);
          Pop.toast(error.message, "error");
        }
      },
      async deleteVk() {
        try {
          if (await Pop.confirm()) {
            Modal.getOrCreateInstance(
              document.getElementById("keepModal")
            ).toggle();
            await vaultKeepsService.delete(AppState.focusKeep);
            Pop.toast("Removed Keep from Vault", "success");
          }
        } catch (error) {
          console.error("[error prefix]", error.message);
          Pop.toast(error.message, "error");
        }
      },
      async deleteKeep() {
        try {
          if (await Pop.confirm()) {
            Modal.getOrCreateInstance(
              document.getElementById("keepModal")
            ).toggle();
            await keepsService.delete(AppState.focusKeep);
            Pop.toast("Keep Deleted", "success");
          }
        } catch (error) {
          console.error("[error prefix]", error.message);
          Pop.toast(error.message, "error");
        }
      },
      goToProfile() {
        router.push({
          name: "Profile",
          params: { id: AppState.focusKeep.creatorId },
        });
        Modal.getOrCreateInstance(
          document.getElementById("keepModal")
        ).toggle();
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.line {
  height: 0.1em;
  width: 80%;
  background-color: rgba(0, 0, 0, 0.488);
}

.fs-2 {
  color: #55efc4;
}

.infostuff {
  margin-top: 30%;
}
</style>
