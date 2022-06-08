<template>
  <Modal id="vaultModal">
    <template #title>
      <h1>Create a Vault</h1>
    </template>
    <template #body>
      <form @submit.prevent="create" id="VaultForm">
        <div class="form-group">
          <label for="name">Title</label>
          <input
            type="name"
            class="form-control"
            id="inputTitle"
            aria-describedby="titleInput"
            placeholder="Enter Vault Title..."
            v-model="editable.name"
            required
          />
          <small id="titleInput" class="form-text text-muted"
            >Tip: Find your keeps easier with a good vault title</small
          >
        </div>
        <div class="form-group">
          <label for="description">Description</label>
          <input
            type="description"
            class="form-control"
            id="inputDescription"
            aria-describedby="inputDescription"
            placeholder="Enter Vault Description..."
            v-model="editable.description"
            required
          />
        </div>
        <div class="form-group">
          <label for="img">Vault Cover</label>
          <input
            type="img"
            class="form-control"
            id="imgInput"
            placeholder="Cover Image Url..."
            v-model="editable.img"
          />
        </div>
        <div class="form-check">
          <input
            type="checkbox"
            class="form-check-input"
            id="privateCheck"
            v-model="editable.isPrivate"
          />
          <label class="form-check-label" for="privateCheck">Private?</label>
        </div>
        <button type="submit" class="btn btn-primary">Submit</button>
      </form>
    </template>
  </Modal>
</template>

<script>
import { ref } from "@vue/reactivity";
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaultsService.js";
import { Modal } from "bootstrap";
import { useRouter } from "vue-router";
export default {
  setup() {
    const editable = ref({});
    const router = useRouter();
    return {
      editable,
      async create() {
        try {
          const newVault = await vaultsService.createVault(editable.value);
          console.log(editable.value);
          document.getElementById("VaultForm").reset();
          Modal.getOrCreateInstance(
            document.getElementById("vaultModal")
          ).toggle();
          // router.push({ path: "vaults/" + newVault.id });
        } catch (error) {
          console.error("[error prefix]", error.message);
          Pop.toast(error.message, "error");
        }
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
