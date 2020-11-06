import ApiService from './api';

export default {
  getCategories() {
    return ApiService.get('categories');
  },

  create(category) {
    return ApiService.post('categories', category);
  },

  update(category) {
    return ApiService.put(`categories/${category.slug}`, category);
  }
}
