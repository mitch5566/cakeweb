import { createStore } from 'vuex';
import axios from 'axios';

export default createStore({
  state: {
    products: []// 存放商品列表
  },
  mutations: {
    setProducts(state, products) {
      state.products = products; // 將 API 返回的商品資料存到 state 中
    }
  },
  actions: {async fetchProducts({ commit }) {
    try {
      // 調用 API 來獲取商品列表
      const response = await axios.get('/api/products');
      const products = response.data.data; // 假設商品資料在 data 裡
      commit('setProducts', products); // 將資料傳給 mutation
    } catch (error) {
      console.error('Error fetching products:', error); // 捕獲並顯示錯誤
    }
  }
},
getters: {
  allProducts(state) {
    return state.products; // 取得商品列表
  }
},
modules: {
  // 可以將不同功能模組化
}
});