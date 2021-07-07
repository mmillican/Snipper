<template>
  <div class="card mx-0 border-0">
    <div class="card-header">
      {{ isNew ? 'Add' : 'Edit' }} Snippet
    </div>

    <div class="card-body">
      <b-form
        @submit.prevent="save"
      >
        <b-form-group
          label-for="snippet-category"
          label="Category"
          label-cols-md="2"
        >
          <b-form-select
            id="snippet-category"
            v-model="category"
            :options="categories"
            value-field="slug"
            text-field="name"
            required
          />
        </b-form-group>

        <b-form-group
          label-for="snippet-name"
          label="Name"
          label-cols-md="2"
        >
          <b-form-input id="snippet-name" v-model="name" required />
        </b-form-group>

        <b-form-group
          label-for="snippet-description"
          label="Description"
          label-cols-md="2"
        >
          <b-form-textarea id="snippet-description" v-model="description" />
        </b-form-group>

        <hr />

        <edit-snippet-file
          v-for="file in files"
          :key="file.order"
          :file="file"
        />

        <b-alert
          variant="info"
          :show="files.length < 1"
        >
          There are no files in this snippet.
        </b-alert>

        <div class="actions my-2 d-flex justify-content-between">
            <b-button
              type="button"
              variant="outline-secondary"
              size="sm"
              @click="addFile"
            >
              Add Another File
            </b-button>

          <div class="">
            <b-button
              type="submit"
              variant="primary"
            >Save</b-button>

            <b-button
              type="button"
              variant="link"
              @click="cancelEdit"
            >Cancel</b-button>
          </div>
        </div>
      </b-form>
    </div>
  </div>
</template>

<script>
import { mapActions } from 'vuex';
import { mapFields } from 'vuex-map-fields';
import EditSnippetFile from './EditSnippetFile.vue';

export default {
  components: {
    EditSnippetFile
  },
  computed: {
    ...mapFields('categories', {
      categories: 'categories',
      selectedCategory: 'selected'
    }),
    ...mapFields('snippets', [
      'isEditing',
      'selected.id',
      'selected.category',
      'selected.language',
      'selected.name',
      'selected.description',
      'selected.userName',
      'selected.files'
    ]),
    isNew() {
      return !this.id;
    }
  },
  created() {
    if (this.selectedCategory && this.selectedCategory.slug) {
      this.category = this.selectedCategory.slug;
    }
  },
  methods: {
    ...mapActions('snippets', [
      'save',
      'cancelEdit'
    ]),
    addFile() {
      this.files.push({});
    }
  }
}
</script>
