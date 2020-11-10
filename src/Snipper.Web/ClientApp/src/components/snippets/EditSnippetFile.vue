<template>
  <div class="card mx-0 mb-4 snippet-file-editor">
    <div class="card-header d-flex justify-content-between">
      <b-form-group
        label-for="file-name"
        label="File name"
        :label-sr-only="true"
        class="my-0"
      >
        <b-form-input
          id="file-name"
          v-model="file.fileName"
          placeholder="filename.txt"
          @change="fileNameChanged"
        />
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

    <div class="card-body">
      <b-form-textarea
        id="file-content"
        v-model="file.content"
        rows="10"
        class="text-monospace"
      />
    </div>
  </div>
</template>

<script>
import { fileTypes } from '@/utils/file-types';

export default {
  props: {
    file: {
      type: Object
    }
  },
  data() {
    return {
      editingFile: { ...this.file }
    }
  },
  computed: {
    fileTypeOptions() {
      const types = [
        { ext: null, name: 'Autodetect', alias: null },
        ...fileTypes
      ]
      return types;
    }
  },
  methods: {
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
