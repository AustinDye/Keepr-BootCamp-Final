<template>
  <Modal id="createKeepModal">
    <template #title>
      <h1>Create a Keep</h1>
    </template>
    <template #body>
      <form @submit.prevent="create" id="KeepForm">
        <div class="form-group">
          <label for="name">Title for Your Keep</label>
          <input
            type="name"
            class="form-control"
            id="inputTitle"
            aria-describedby="titleInput"
            placeholder="Keep Title..."
            v-model="editable.name"
            required
          />
          <small id="titleInput" class="form-text text-muted"></small>
        </div>
        <div class="form-group">
          <label for="description">Description of your Keep</label>
          <input
            type="description"
            class="form-control"
            id="inputDescription"
            aria-describedby="inputDescription"
            placeholder="Keep Description..."
            v-model="editable.description"
            required
          />
        </div>
        <div class="form-group">
          <label for="img">Keep Url</label>
          <input
            type="img"
            class="form-control"
            id="imgInput"
            placeholder="Keep Url..."
            v-model="editable.img"
          />
        </div>
        <button type="submit" class="btn btn-primary">Submit</button>
      </form>
    </template>
  </Modal>
</template>

<script>
import { ref } from "@vue/reactivity";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";
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
          const newKeep = await keepsService.create(editable.value);
          console.log(editable.value);
          document.getElementById("KeepForm").reset();
          Modal.getOrCreateInstance(
            document.getElementById("createKeepModal")
          ).toggle();
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
