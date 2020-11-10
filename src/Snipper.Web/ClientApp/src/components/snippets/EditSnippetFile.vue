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
  // watch: {
  //   editingFile(val) {
  //     this.file = { ...this.editingFile };
  //   }
  // },
  computed: {
    fileTypeOptions() {
      const types = [
        { ext: null, name: 'Autodetect', alias: null },
        ...fileTypes
      ]
      return types;
    }
  }
}
</script>
