<template>
  <b-navbar toggleable="lg" type="dark" variant="dark">
    <div class="container-fluid">
      <b-navbar-brand to="/">Snipper</b-navbar-brand>

      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav>
          <b-nav-item to="/search">Search</b-nav-item>
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
export default {
  data() {
    return {
      isAuthenticated: false,
      searchQuery: null
    };
  },
  methods: {
    searchSnippets() {
      this.$router.push({ name: 'search', query: { query: this.searchQuery } });
    }
  }
};
</script>
