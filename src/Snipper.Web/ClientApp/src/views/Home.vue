<template>
  <div class="row h-100">
    <div class="col-md-2 border-right">
      <categories />
    </div>
    <div class="col-md-2 border-right">
      <snippets />
    </div>
    <div class="col-md-8 px-0">
      <view-snippet />
    </div>
  </div>
</template>

<script>
import Categories from '@/components/Categories.vue';
import Snippets from '@/components/snippets/SnippetList.vue';
import ViewSnippet from '@/components/snippets/ViewSnippet.vue';

export default {
  components: {
    Categories,
    Snippets,
    ViewSnippet
  }
}
</script>
