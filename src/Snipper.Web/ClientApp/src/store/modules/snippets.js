import { getField, updateField } from 'vuex-map-fields';
import SnippetService from '@/services/snippets';

const getDefaultSelectedState = () => {
  return {

  };
};

const state = {
  isLoading: false,
  snippets: [],
  selected: {
    ...getDefaultSelectedState()
  }
};

const getters = {
  getField
};

const mutations = {
  updateField,

  SET_IS_LOADING(state, value) {
    state.isLoading = value;
  },

  SET_SNIPPETS(state, value) {
    state.snippets = value;
  }
};

const actions = {
  getByCategory({ commit }, categorySlug) {
    commit('SET_IS_LOADING', true);

    return SnippetService.getByCategory(categorySlug).then(response => {
      commit('SET_SNIPPETS', response.data);

      commit('SET_IS_LOADING', false);
    });
  }
};

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
};
