<template>
  <div class="snippet-list">
    <div v-if="!selectedCategory">
      <h4>Select a category</h4>
    </div>

    <ul class="nav flex-column my-2" v-if="!isLoading">
      <li class="nav-item text-uppercase d-flex justify-content-between">
        <h6 class="align-bottom">{{ selectedCategory.name }}</h6>
        <b-button size="sm" variant="outline-success" @click="showEdit">
          Add
        </b-button>
      </li>

      <li
        class="nav-item"
        v-for="snippet in snippets"
        :key="snippet.id"
      >
        <router-link
          class="nav-link"
          :to="{name: 'viewSnippet', params: {
            categorySlug: selectedCategory.slug,
            id: snippet.id
           } }">
          {{ snippet.name }}
        </router-link>
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
      console.log('snippet category changed', val);

      this.getSnippets();
    }
  },
  created() {
    this.getSnippets();
  },
  methods: {
    ...mapActions('snippets', [
      'getByCategory'
    ]),

    getSnippets() {
      if (this.selectedCategory && this.selectedCategory.slug) {
        console.log('get snippets for category', this.selectedCategory);
        this.getByCategory(this.selectedCategory.slug);
      }
    }
  }
}
</script>
