import Vue from 'vue'
import BootstrapVue from 'bootstrap-vue';
import App from './App.vue'
import router from './router'
import store from './store'

import 'bootstrap/scss/bootstrap.scss';
import 'bootstrap-vue/dist/bootstrap-vue.css';

import 'vue-code-highlight/themes/prism.css';
import 'prism-es6/components/prism-sql';
import 'prism-es6/components/prism-csharp';
import 'prism-es6/components/prism-json';
import 'prism-es6/components/prism-markdown';
import 'prism-es6/components/prism-yaml';

Vue.use(BootstrapVue);

Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
