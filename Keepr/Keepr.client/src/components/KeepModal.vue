<template>
  <Modal id="keepModal">
    <template #title>
      <h3>{{ focusKeep.name }}</h3>
    </template>
    <template #body>
      <div class="row">
        <div class="col-6">
          <img class="img-fluid" :src="focusKeep.img" alt="" />
        </div>
        <div class="col-6">
          <span class="d-flex justify-content-between">
            <p>{{ focusKeep.views }}</p>
            <p>{{ focusKeep.kept }}</p>
            <p>15</p>
          </span>
          <h3 class="text-center">{{ focusKeep.name }}</h3>
          <p>{{ focusKeep.description }}</p>
          <div class="line"></div>
          <span class="d-flex">
            <div class="btn btn-info" @click="createVault()">Add To Vault</div>
          </span>
        </div>
      </div>
    </template>
  </Modal>
</template>

<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";
export default {
  setup() {
    return {
      focusKeep: computed(() => AppState.focusKeep),
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
