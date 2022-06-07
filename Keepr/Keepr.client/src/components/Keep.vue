<template>
  <figure>
    <img class="" :src="keep.img" alt="" />
  </figure>
</template>

<script>
export default {
  props: {
    keep: {
      type: Object,
    },
  },
  setup() {
    return {};
  },
};
</script>

<style lang="scss" scoped></style>
