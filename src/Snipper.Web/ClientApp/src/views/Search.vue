<template>
  <div class="search">
    <h1>Search Results</h1>

    <template v-if="results && results.length > 0">

      <h3>{{ results.length }} results matched your query for "{{ query }}"</h3>

      <div class="row">
        <div class="col-md-8 search-results">
          <div
            class="card mx-0 mb-4 result"
            v-for="result in results"
            :key="result.id"
          >
            <div class="card-header d-flex justify-content-between">
              <h5>{{ result.name }}</h5>
              <div class="category">
                {{ result.category }}
              </div>
            </div>

            <div class="card-body">
              <vue-code-highlight :language="result.language">
                {{ result.content }}
              </vue-code-highlight>
            </div>
          </div>
        </div>
      </div>
    </template>

    <h3 v-else>
      No results match your query for "{{ query }}"
    </h3>
  </div>
</template>

<script>
import { mapActions } from 'vuex';
import { mapFields } from 'vuex-map-fields'
import { component as VueCodeHighlight } from 'vue-code-highlight';

export default {
  components: {
    VueCodeHighlight
  },
  computed: {
    ...mapFields('snippets', [
      'search.query',
      'search.results'
    ])
  },
  async created() {
    await this.doSearch();
  },
  watch: {
    $route: 'doSearch'
  },
  methods: {
    ...mapActions('snippets', [
      'search',
      'clearSearch'
    ]),
    async doSearch() {
      var query = this.$route.query.query;
      if (query) {
        await this.search(query);
      }
    }
  }
}
</script>
