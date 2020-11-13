import ApiService from './api';

export default {
  getByCategory(categorySlug) {
    return ApiService.get(`categories/${categorySlug}/snippets`);
  },

  create(snippet) {
    return ApiService.post('snippets', snippet);
  },

  update(snippet) {
    return ApiService.put(`snippets/${snippet.id}`, snippet);
  },

  delete(snippet) {
    return ApiService.delete(`snippets/${snippet.id}`);
  }
}
