<template>
  <div class="row h-100">
    <div class="col-md-2 border-right">
      <categories />
    </div>
    <div class="col-md-2">
      <snippets />
    </div>
    <div class="col-md-8">
      Col 3
    </div>
  </div>
</template>

<script>
import Categories from '@/components/Categories.vue';
import Snippets from '@/components/snippets/SnippetList.vue';

export default {
  components: {
    Categories,
    Snippets
  }
}
</script>
