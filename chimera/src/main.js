import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import router from './router'
import store from './store' // 引入 Vuex store
//createApp(App).use(router.router).mount('#app')

 createApp(App).use(router.router).use(store).mount('#app')