<template>
  <div class="snippet-details">
    <div class="view-snippet" v-if="id && !isEditing">
      <div class="card mx-0 border-0">
        <div class="card-header d-flex justify-content-between">
          <h5>{{ name }}</h5>

          <div class="actions">
            <b-button
              variant="outline-secondary"
              class="mr-1"
              size="sm"
              @click="edit"
            >
              Edit
            </b-button>

            <b-button
              variant="outline-danger"
              size="sm"
              @click="remove"
            >
              Delete
            </b-button>
          </div>
        </div>

        <div class="card-body">
          <p class="snippet-description font-italic" v-if="description">
            {{ description }}
          </p>
        </div>
      </div>

      <div class="snippet-files">
        <snippet-file
          v-for="file in files"
          :key="file.fileName"
          :file="file"
        />
      </div>
    </div>

    <edit-snippet v-else-if="isEditing" />
  </div>
</template>

<script>
import { mapActions } from 'vuex';
import { mapFields } from 'vuex-map-fields';
import SnippetFile from './SnippetFile.vue';
import EditSnippet from './EditSnippet.vue';

export default {
  components: {
    SnippetFile,
    EditSnippet
  },
  computed: {
    ...mapFields('snippets', [
      'isEditing',
      'selected.id',
      'selected.category',
      'selected.language',
      'selected.name',
      'selected.description',
      'selected.userName',
      'selected.files',
      'selected.createdOn',
      'selected.updatedOn'
    ])
  },
  methods: {
    ...mapActions('snippets', [
      'edit',
      'remove'
    ])
  }
}
</script>
