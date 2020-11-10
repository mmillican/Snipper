<template>
  <div class="card mx-0 mb-4 snippet-file-editor">
    <div class="card-header d-flex justify-content-between">
      <b-form-group
        label-for="file-name"
        label="File name"
        :label-sr-only="true"
        class="my-0"
      >
        <b-input-group>
          <b-form-input
            id="file-name"
            v-model="file.fileName"
            placeholder="filename.txt"
            @change="fileNameChanged"
          />
          <b-input-group-append>
            <b-button
              type="button"
              variant="outline-danger"
              @click="remove"
              :disabled="!canRemove"
              :title="removeTooltip"
            >
              <b-icon-trash
              />
            </b-button>
          </b-input-group-append>
        </b-input-group>
      </b-form-group>

      <div>
        <b-form-group
          label-for="file-type"
          label="File type"
          :label-sr-only="true"
          class="my-0"
        >
          <b-form-select
            id="file-type"
            v-model="file.language"
            :options="fileTypeOptions"
            text-field="name"
            value-field="alias"
          />
        </b-form-group>
      </div>
    </div>

    <div class="card-body p-0">
      <b-form-textarea
        id="file-content"
        v-model="file.content"
        rows="7"
        class="text-monospace border-0"
      />
    </div>
  </div>
</template>

<script>
import { fileTypes } from '@/utils/file-types';
import { mapActions, mapGetters } from 'vuex';
import { BIconTrash } from 'bootstrap-vue';

export default {
  props: {
    file: {
      type: Object
    }
  },
  components: {
    BIconTrash
  },
  data() {
    return {
      editingFile: { ...this.file }
    }
  },
  computed: {
    ...mapGetters('snippets', [
      'fileCount'
    ]),
    canRemove() {
      return this.fileCount > 1;
    },
    removeTooltip() {
      return this.canRemove
        ? 'Remove this file'
        : 'This is the only file in this snippet and cannot be removed.';
    },
    fileTypeOptions() {
      const types = [
        { ext: null, name: 'Autodetect', alias: null },
        ...fileTypes
      ]
      return types;
    }
  },
  methods: {
    ...mapActions('snippets', [
      'removeFile'
    ]),
    remove() {
      this.removeFile(this.file)
    },
    fileNameChanged(val) {
      if (!val) {
        return;
      }

      const extensionRegex = /(?:\.([^.]+))?$/
      const extension = extensionRegex.exec(val)[1];

      const fileType = fileTypes.find(x => x.ext === extension);
      this.file.language = fileType ? fileType.alias : null;
    }
  }
}
</script>
