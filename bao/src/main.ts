import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import router from './router'
import store from './store' // 引入 Vuex store



//先不用 bootstrap
// import 'bootstrap/dist/css/bootstrap.min.css';
// import 'bootstrap';

// import router from './router'
// import store from './store'

createApp(App).use(router.router).use(store).mount('#app')
