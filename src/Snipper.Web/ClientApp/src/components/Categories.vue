<template>
  <div id="categories" class="my-2">
    <transition name="slide-fade">
      <ul class="nav flex-column" v-if="!isLoading">
        <li class="nav-item text-uppercase d-flex justify-content-between">
          <h6 class="align-bottom">Categories</h6>
          <b-button size="sm" variant="outline-success" @click="showEdit" v-if="!isEditing">
            Add
          </b-button>
        </li>
        <li class="nav-item" v-if="isEditing">
          <b-form
            inline
            @submit.prevent="save"
          >
            <label class="sr-only" for="category-form-name">Category Name</label>
            <b-input
              id="category-form-name"
              size="sm"
              v-model="name"
              class="mb-2 mr-sm-2 mb-sm-0"
              placeholder="Category name"
            />

            <b-button type="submit" variant="primary" size="sm">Save</b-button>
          </b-form>
        </li>

        <li
          class="nav-item"
          v-for="category in categories"
          :key="category.slug"
        >
          <router-link
            class="nav-link"
            :to="{name: 'snippetList', params: { categorySlug: category.slug } }"
            :class="{ disabled: selected && selected.slug === category.slug }">
            {{ category.name }}
          </router-link>
        </li>
      </ul>
    </transition>
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
    ...mapFields('categories', [
      'isLoading',
      'categories',
      'selected',
      'editing.isEditing',
      'editing.slug',
      'editing.name'
    ])
  },

  watch: {
    $route(val) {
      const slug = val.params.categorySlug;
      this.select(slug);

      this.clearSelectedSnippet();
    }
  },

  created() {
    this.getAll().then(() => {
      this.select(this.$route.params.categorySlug);
    });
  },

  methods: {
    ...mapActions('categories', [
      'getAll',
      'select',
      'showEdit',
      'save'
    ]),
    ...mapActions('snippets', [
      'clearSelectedSnippet'
    ])
  }
};
</script>
