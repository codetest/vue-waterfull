// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import MainComponent from "./components/Main.vue"
import ItemComponent from "./components/Item.vue"
import BigItemComponent from "./components/BigItem.vue"

Vue.component("ItemComponent", ItemComponent)
Vue.component("MainComponent", MainComponent)
Vue.component("BigItemComponent", BigItemComponent)
Vue.config.productionTip = false

Vue.prototype.BigImageInView = false
/* eslint-disable no-new */
new Vue({
  el: '#app',
  render: h => h(App),
})
