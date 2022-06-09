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
            <div class="col-12 d-flex infostuff justify-content-between">
              <div>
                <div class="dropdown">
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

                  <ul class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                    <p v-for="v in vaults" :key="v.id" :vault="v" :value="v.id">
                      <a class="dropdown-item" @click="createVk(v)">
                        {{ v.name }}</a
                      >
                    </p>
                  </ul>
                </div>
                <!-- <form @submit.prevent="createVk()">
                  <select
                    class="form-control"
                    aria-label="select vault"
                    v-model="editable.vaultId"
                  >
                    <option selected>Add to a Keep</option>
                    <option
                      v-for="v in vaults"
                      :key="v.id"
                      :vault="v"
                      :value="v.id"
                    >
                      <p type="submit">{{ v.name }}</p>
                    </option>
                  </select>
                  <button class="btn btn-info">Submit</button>
                </form>-->
              </div>

              <button
                v-if="focusVault.creatorId == account.id"
                class="btn btn-danger w-50 mt-3"
                @click="deleteVk"
              >
                Remove From Vault
              </button>
              <i
                class="mdi mdi-trash-can fs-1"
                @click="deleteKeep"
                v-if="focusKeep.creatorId == focusKeep.creator.id"
              ></i>
              <div
                v-if="focusKeep.creator.id"
                class="rounded accountbar d-flex selectable"
              >
                <img
                  @click="goToProfile(focusKeep.creatorId)"
                  class="user-img rounded"
                  :src="focusKeep.creator.picture"
                />
                <p class="mx-3 lighten-30 my-1">
                  {{ focusKeep.creator.name }}
                </p>
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
      goToProfile(creatorId) {
        router.push({
          name: "Profile",
          params: { id: creatorId },
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
.user-img {
  width: 2em;
  height: 2em;
}

.line {
  height: 0.1em;
  width: 80%;
  background-color: rgba(0, 0, 0, 0.488);
}

.infostuff {
  margin-top: 60%;
}
.fs-2 {
  color: #55efc4;
}
</style>
