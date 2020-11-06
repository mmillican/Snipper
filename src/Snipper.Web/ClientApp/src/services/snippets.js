import ApiService from './api';

export default {
  getByCategory(categorySlug) {
    return ApiService.get(`categories/${categorySlug}/snippets`);
  },

  create(snippet) {
    return ApiService.post('snippets', snippet);
  }

  // update(category) {
  //   return ApiService.put(`categories/${category.slug}`, category);
  // }
}
