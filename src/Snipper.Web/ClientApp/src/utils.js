import store from './store/index';
import { ADD_TOAST } from './store/modules/toasts';

export default {
  emptyGuid: '00000000-0000-0000-0000-000000000000'
};

export function addSuccessToast(message) {
  store.dispatch(ADD_TOAST, { type: 'success', message, dismissAfter: 5000 }, { root: true });
}

export function addInfoToast(message) {
  store.dispatch(ADD_TOAST, { type: 'info', message, dismissAfter: 5000 }, { root: true });
}

export function addWarningToast(message) {
  store.dispatch(ADD_TOAST, { type: 'warning', message, dismissAfter: 5000 }, { root: true });
}

export function addErrorToast(message) {
  store.dispatch(ADD_TOAST, { type: 'danger', message, dismissAfter: 5000 }, { root: true });
}
