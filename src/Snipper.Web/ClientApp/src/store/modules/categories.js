import { getField, updateField } from 'vuex-map-fields';
import CategoryService from '@/services/categories';
import { addErrorToast, addSuccessToast } from '@/utils';

const getDefaultSelectedState = () => {
  return {
    slug: null,
    name: null
  };
};

const getDefaultEditingState = () => {
  return {
    isEditing: false,
    ...getDefaultSelectedState()
  };
};

const state = {
  isLoading: false,
  categories: [],
  isEditing: false,
  selected: {
    ...getDefaultSelectedState()
  },
  editing: {
    ...getDefaultEditingState()
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
  SET_CATEGORIES(state, value) {
    state.categories = value;
  },
  SET_SELECTED(state, value) {
    state.selected = value;
  },
  RESET_SELECTED(state) {
    state.selected = Object.assign(state.selected, getDefaultSelectedState());
  },
  SET_IS_EDITING(state, value) {
    state.editing.isEditing = value;
  },
  SET_EDITING(state, value) {
    state.editing.isEditing = true;
    state.editing = Object.assign(state.editing, value);
  },
  RESET_EDITING(state) {
    state.editing = Object.assign(state.editing, getDefaultEditingState());
  }
};

const actions = {
  getAll({ commit }) {
    commit('SET_IS_LOADING', true);
    return CategoryService.getCategories().then(response => {
      commit('SET_CATEGORIES', response.data);

      commit('SET_IS_LOADING', false);
    });
  },

  select({ commit, state }, slug) {
    const category = state.categories.find(x => x.slug === slug);
    if (category) {
      commit('SET_SELECTED', category);
    }
  },

  showEdit({ commit }, category) {
    commit('SET_IS_EDITING', true);
    if (category) {
      commit('SET_SELECTED', category);
    }
  },

  closeEdit({ commit }) {
    commit('RESET_EDITING');
  },

  save({ dispatch, state }) {
    if (!state.editing.slug) {
      return CategoryService.create(state.editing).then(response => {
        dispatch('closeEdit');
        dispatch('getAll');

        addSuccessToast('The category has been created.');
      }).catch(_ => {
        addErrorToast('There was an error creating the category. Try again.');
      });
    } else {
      return CategoryService.update(state.editing).then(response => {
        dispatch('closeEdit');
        dispatch('getAll');

        addSuccessToast('The category has been updated.');
      }).catch(_ => {
        addErrorToast('There was an error updating the category. Try again.');
      });
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
