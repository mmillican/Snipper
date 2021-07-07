<template>
  <div class="card mx-0 border-0">
    <div class="card-header d-flex justify-content-between">
      <span class="file-name">
        {{ file.fileName }}
      </span>

      <div class="file-actions">
        <!-- <b-button
          type="button"
          size="sm"
          class="py-0"
          variant="outline-secondary"
        >
          Raw
        </b-button>&nbsp;

        <b-button
          type="button"
          size="sm"
          class="py-0"
          variant="outline-secondary"
        >
          Copy
        </b-button> -->
      </div>
    </div>

    <div class="card-body">
      <vue-code-highlight v-if="file && file.content" :language="'sql'">
        <pre>{{ file.content }}</pre>
      </vue-code-highlight>
    </div>
  </div>
</template>

<script>
import { component as VueCodeHighlight } from 'vue-code-highlight';

export default {
  props: {
    file: {
      type: Object,
      required: true
    }
  },
  components: {
    VueCodeHighlight
  }
}
</script>

<style>
:not(pre) > code[class*="language-"], pre[class*="language-"] {
  background: none !important;
}
</style>
