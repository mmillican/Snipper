// Based on https://github.com/ktsn/vuex-toast/blob/master/src/module.js

const prefix = 'TOAST';
const ADD = `${prefix}_ADD_MESSAGE`;
const REMOVE = `${prefix}_REMOVE_MESSAGE`;

export {
  ADD as ADD_TOAST,
  REMOVE as REMOVE_TOAST
};

const state = {
  toasts: []
};

export function createToastModule() {
  let maxToastId = 0;

  const getters = {
    toasts: (state) => state.toasts
  };

  const mutations = {
    [ADD] (state, value) {
      state.toasts.push(value);
    },

    [REMOVE] (state, id) {
      state.toasts = state.toasts.filter(x => x.id !== id);
    }
  };

  const actions = {
    [ADD] ({ commit }, toast) {
      toast.id = ++maxToastId;

      commit(ADD, toast);
      setTimeout(() => commit(REMOVE, toast.id), toast.dismissAfter);
    },

    [REMOVE] ({ commit }, id) {
      commit(REMOVE, id);
    }
  };

  return {
    state,
    getters,
    mutations,
    actions
  };
}
