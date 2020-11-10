<template>
  <div class="snippet-list">
    <div v-if="!selectedCategory">
      <h4>Select a category</h4>
    </div>

    <ul class="nav flex-column my-2" v-if="!isLoading">
      <li class="nav-item text-uppercase d-flex justify-content-between">
        <h6 class="align-bottom">{{ selectedCategory.name }}</h6>
        <b-button size="sm" variant="outline-success" @click="addNew">
          Add
        </b-button>
      </li>

      <li
        class="nav-item"
        v-for="snippet in snippets"
        :key="snippet.id"
      >
        <a
          class="nav-link"
          href="#"
          @click.prevent="viewSnippet(snippet)"
        >
          {{ snippet.name }}
        </a>
      </li>

    </ul>
  </div>
</template>

<script>
import { mapActions } from 'vuex';
import { mapFields } from 'vuex-map-fields';

export default {
  data() {
    return {

    };
  },
  computed: {
    ...mapFields('categories', {
      selectedCategory: 'selected'
    }),
    ...mapFields('snippets', [
      'isLoading',
      'snippets'
    ])
  },
  watch: {
    selectedCategory(val) {
      this.getSnippets();
    }
  },
  created() {
    this.getSnippets();
  },
  methods: {
    ...mapActions('snippets', [
      'getByCategory',
      'select',
      'addNew'
    ]),

    getSnippets() {
      if (this.selectedCategory && this.selectedCategory.slug) {
        this.getByCategory(this.selectedCategory.slug);
      }
    },

    viewSnippet(snippet) {
      this.select(snippet);

      // this.$router.push({
      //     name: 'viewSnippet',
      //     params: { categorySlug: this.selectedCategory.slug, id: snippet.id }
      //   }
      // );
    }
  }
}
</script>
