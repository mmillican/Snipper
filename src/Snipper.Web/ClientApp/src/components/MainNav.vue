<template>
  <b-navbar toggleable="lg" type="dark" variant="dark">
    <div class="container-fluid">
      <b-navbar-brand to="/">Snipper</b-navbar-brand>

      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav>
          <template v-if="isAuthenticated">
            <b-nav-item to="/albums/mine">My Albums</b-nav-item>
            <b-nav-item :to="{name: 'roadsList'}">Railroads</b-nav-item>
            <b-nav-item :to="{name: 'locomotiveList'}">Locomotives</b-nav-item>
            <b-nav-item :to="{name: 'userList'}">Users</b-nav-item>
          </template>
        </b-navbar-nav>

        <!-- Right aligned nav items -->
        <b-navbar-nav class="ml-auto">
          <form
            @submit.prevent="searchSnippets"
            class="form-inline"
          >
            <input
              type="search"
              v-model="searchQuery"
              class="form-control mr-sm-2"
              placeholder="Search..."
              aria-label="Search"
            />

            <button
              type="submit"
              class="btn btn-outline-success my-2 my-sm-0 sr-only"
            >
              Search
            </button>
          </form>
        </b-navbar-nav>
      </b-collapse>
    </div>
  </b-navbar>
</template>

<script lang="ts">
import { mapActions } from 'vuex';
import { mapFields } from 'vuex-map-fields';

export default {
  data() {
    return {
      isAuthenticated: false
    };
  },
  computed: {
    ...mapFields('snippets', {
      searchQuery: 'search.query'
    })
  },
  // created() {
  //   this.authenticate();
  // },
  // computed: {
  //   ...mapGetters([
  //     'currentUser'
  //   ])
  // },
  // watch: {
  //   $route: 'authenticate'
  // },
  methods: {
    ...mapActions('snippets', [
      'search'
    ]),
    searchSnippets() {
      console.log('search');
      this.search()
    }
    // async authenticate() {
    //   this.isAuthenticated = await this.$auth.isAuthenticated();
    // },
    // async logout() {
    //   await this.$auth.logout();
    //   await this.authenticate();
    // }
  }
};
</script>
