<template>
  <div id="app" class="h-100">
    <main-nav />

    <div class="container-fluid">
      <router-view/>
    </div>

  </div>
</template>

<script>
import MainNav from '@/components/MainNav.vue';

export default {
  components: {
    MainNav
  }
}
</script>

<style lang="scss">
html,body {
  height: 100%;
}
.container-fluid {
  height: calc(100% - 56px);
}
.slide-fade-enter-active {
  transition: all .5s ease;
}
.slide-fade-leave-active {
  transition: all .8s cubic-bezier(1.0, 0.5, 0.8, 1.0);
}
.slide-fade-enter, .slide-fade-leave-to
/* .slide-fade-leave-active below version 2.1.8 */ {
  transform: translateX(10px);
  opacity: 0;
}
</style>
