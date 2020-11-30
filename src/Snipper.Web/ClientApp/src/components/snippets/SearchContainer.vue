<template>
  <b-modal
    v-model="show"
    :title='`${results.length} Results for "${query}"`'
    size="lg"
    no-close-on-backdrop
    hide-footer
    body-class="px-0"
    @hidden="clearSearch"
  >
    <div class="search-results">
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
            <pre>{{ result.content }}</pre>
          </vue-code-highlight>
        </div>
      </div>
    </div>
  </b-modal>
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
      'search.show',
      'search.query',
      'search.results'
    ])
  },
  methods: {
    ...mapActions('snippets', [
      'clearSearch'
    ])
  }
}
</script>
