import { getField, updateField } from 'vuex-map-fields';
import SnippetService from '@/services/snippets';

const getDefaultSelectedState = () => {
  return {
    id: null,
    category: null,
    language: null,
    name: null,
    description: null,
    userName: null,
    files: [],
    createdOn: null,
    updatedOn: null
  };
};

const state = {
  isLoading: false,
  isEditing: false,
  snippets: [],
  selected: {
    ...getDefaultSelectedState()
  }
};

const getters = {
  getField,
  fileCount: state =>
    state.selected ? state.selected.files.length : 0
};

const mutations = {
  updateField,

  SET_IS_LOADING(state, value) {
    state.isLoading = value;
  },

  SET_IS_EDITING(state, value) {
    state.isEditing = value;
  },

  SET_SNIPPET_FILES(state, value) {
    state.selected.values = value;
  },

  SET_SNIPPETS(state, value) {
    state.snippets = value;
  },

  SET_SELECTED(state, value) {
    state.selected = value;
  },

  RESET_EDITING(state) {
    state.isEditing = false;
    // state.selected = Object.assign(state.selected, getDefaultSelectedState());
  }
};

const actions = {
  getByCategory({ commit }, categorySlug) {
    commit('SET_IS_LOADING', true);

    return SnippetService.getByCategory(categorySlug).then(response => {
      commit('SET_SNIPPETS', response.data);

      commit('SET_IS_LOADING', false);
    });
  },

  select({ commit }, snippet) {
    commit('SET_SELECTED', snippet);
  },

  addNew({ commit }) {
    commit('SET_IS_EDITING', true);
    commit('SET_SELECTED', getDefaultSelectedState());
  },

  edit({ commit }) {
    commit('SET_IS_EDITING', true);
  },

  closeEdit({ commit }) {
    commit('RESET_EDITING');
  },

  cancelEdit({ commit }) {
    commit('SET_IS_EDITING', false);
  },

  save({ dispatch, state }) {
    if (!state.selected.id) {
      return SnippetService.create(state.selected).then(() => {
        dispatch('getByCategory', state.selected.category);
        dispatch('closeEdit');
      });
    } else {
      return SnippetService.update(state.selected).then(() => {
        dispatch('closeEdit');
      });
    }
  },

  removeFile({ commit, state }, file) {
    const index = state.selected.files.indexOf(file);
    if (index > -1) {
      const files = state.selected.files.splice(index, 1);
      commit('SET_SNIPPET_FILES', files);
    }
  }
};

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
};
