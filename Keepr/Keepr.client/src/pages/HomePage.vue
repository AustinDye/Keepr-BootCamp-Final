<template>
  <Keep v-for="k in keeps.slice(0, 20)" :key="k.id" :keep="k" />
</template>

<script>
import { computed, onMounted } from "@vue/runtime-core";
import { keepsService } from "../services/KeepsService";
import { AppState } from "../AppState";
import Pop from "../utils/Pop";
export default {
  name: "Home",
  setup() {
    onMounted(async () => {
      try {
        await keepsService.getAll();
      } catch (error) {
        Pop.toast("Something went wrong", error);
      }
    });
    return {
      keeps: computed(() => AppState.keeps),
      async create() {
        try {
          await keepsService.create();
        } catch (error) {
          Pop.toast("Something went wrong", error);
        }
      },
    };
  },
};
</script>

<style scoped lang="scss"></style>
