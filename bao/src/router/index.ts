import { createWebHistory, createRouter } from 'vue-router'
import HomeView from '../pages/HomeView.vue';
import AboutView from '../pages/AboutView.vue';
import MemberLoginView from "../pages/Memberloginview.vue";
import ProductView from '../pages/ProductView.vue';
//import HomeView from './pages/HomeView.vue'
// import ProductView from './pages/ProductView.vue'

const routes = [
  { path: '/', component: HomeView },
  { path: '/products', component: ProductView },
  { path: '/about', component: AboutView },
  {path: '/login',component: MemberLoginView}

//   { path: '/products', component: ProductView },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
});

// 將router export出去
export default {
    router
}