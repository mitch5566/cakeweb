import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import router from './router'



// import 'bootstrap/dist/css/bootstrap.min.css';
// import 'bootstrap';

// import router from './router'
// import store from './store'

createApp(App).use(router.router).mount('#app')
