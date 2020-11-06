import Vue from 'vue'
import Vuex from 'vuex'
import CategoriesModule from './modules/categories';
import SnippetsModule from './modules/snippets';

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
  },
  mutations: {
  },
  actions: {
  },
  modules: {
    categories: CategoriesModule,
    snippets: SnippetsModule
  }
});
