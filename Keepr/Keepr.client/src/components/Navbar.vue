<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-success px-3">
    <div
      class="collapse navbar-collapse justify-content-between"
      id="navbarText"
    >
      <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
        <div class="d-flex flex-column align-items-center">
          <img alt="logo" src="../assets/img/Logo.png" height="60" />
        </div>
      </router-link>
      <div>
        <form action="" class="">
          <div class="input-group">
            <input
              type="text"
              class="form-control form-control-lg bg-white"
              placeholder="Search.."
            />
            <div type="submit" class="input-group-text bg-white">
              <i class="mdi mdi-magnify"></i>
            </div>
          </div>
        </form>
      </div>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
export default {
  setup() {
    return {};
  },
};
</script>

<style scoped>
a:hover {
  text-decoration: none;
}
.nav-link {
  text-transform: uppercase;
}
.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
</style>
