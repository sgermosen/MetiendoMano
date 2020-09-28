// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'

// Element Ui
import Element from 'element-ui'
import locale from 'element-ui/lib/locale/lang/es'
import 'element-ui/lib/theme-chalk/index.css'

// App
import App from './App'

// Vue Router
import router from './router'

// Vuex: for services, shared components, etc
import store from './store/index'

// Vue Masonry
import {VueMasonryPlugin} from 'vue-masonry';

// Our Style
import '../static/style.css'

Vue.config.productionTip = false

Vue.use(Element, {
  locale
})

Vue.use(VueMasonryPlugin)

// MomentJs for Vue
Vue.use(require('vue-moment'))

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  template: '<App/>',
  components: {
    App
  }
})
