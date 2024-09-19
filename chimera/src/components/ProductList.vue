<template>
    <div class="cart-container">
      <h1>商品列表</h1>
  
      <!-- 顯示商品列表，即使尚未加載商品數據 -->
      <ul v-if="allProducts.length > 0">
        <li v-for="(product, index) in allProducts" :key="product.productID">
          <div class="product-item">
            <span>{{ product.productName }} - {{ product.price }}元</span>
            <span>庫存: {{ product.stockQuantity }}</span>
  
            <!-- 購買數量輸入框 -->
            <div class="quantity-container">
              <button @click="decreaseQuantity(index)">-</button>
              <input type="number" v-model.number="product.purchaseQuantity" min="0" :max="product.stockQuantity" />
              <button @click="increaseQuantity(index)">+</button>
            </div>
  
            <span>購買數量: {{ product.purchaseQuantity }}</span>
          </div>
        </li>
      </ul>
  
      <!-- 如果商品列表是空的，顯示 "加載中" -->
      <div v-else>
        加載中...
      </div>
  
      <div class="total" v-if="allProducts.length > 0">
      <span>總購買數量: {{ totalPurchaseQuantity }}</span>
      <span>總價: {{ totalPrice }} 元</span> <!-- 添加總價顯示 -->
    </div>
  
      <button @click="submitOrder" v-if="allProducts.length > 0">送出訂單</button>
    </div>
  </template>
  
  <script setup>
  import { ref, computed, onMounted } from 'vue';
  import { useStore } from 'vuex';
  
  const store = useStore();
  
  // 使用 ref 初始化商品列表
  const allProducts = ref([]);
  
  // 確保頁面加載時從 Vuex 獲取商品列表
  onMounted(async () => {
    document.title = "商品列表 - 我的蛋糕店";
  
    // 等待 Vuex 完成商品數據的獲取
    await store.dispatch('fetchProducts');
  
    // 將從 Vuex 獲取的商品數據複製到本地
    allProducts.value = store.getters.allProducts.map(product => ({
      ...product,
      purchaseQuantity: 0, // 預設購買數量為 0
    }));
  });
  
  // 增加數量，最大不能超過庫存
  const increaseQuantity = (index) => {
    const product = allProducts.value[index];
    if (product.purchaseQuantity < product.stockQuantity) {
      product.purchaseQuantity++;
    }
  };
  
  // 減少數量，最小不能低於 0
  const decreaseQuantity = (index) => {
    const product = allProducts.value[index];
    if (product.purchaseQuantity > 0) {
      product.purchaseQuantity--;
    }
  };
  
  // 計算總購買數量
  const totalPurchaseQuantity = computed(() => {
    return allProducts.value.reduce((total, product) => total + product.purchaseQuantity, 0);
  });
  
// 計算總價
const totalPrice = computed(() => {
  return allProducts.value.reduce((total, product) => total + (product.purchaseQuantity * product.price), 0);
});



  // 送出訂單
  const submitOrder = () => {
    const order = allProducts.value
      .filter(product => product.purchaseQuantity > 0)
      .map(product => ({
        ID: product.productID,
        名: product.productName,
        數量: product.purchaseQuantity
      }));
  
    // 使用 alert 提示購買數量
    alert(JSON.stringify(order, null, 2));
  };
  </script>


<style scoped>

.cart-container {
  background-color: #FFF7D1; /* 粉黃色背景 */
  padding: 20px;
  border-radius: 8px;
}

h1 {
  color: #333; /* 深色文字，確保清晰度 */
}

ul {
  list-style: none;
  padding: 0;
}

.product-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px 0;
  border-bottom: 1px solid #E0E0E0;
}

.quantity-container {
  display: flex;
  align-items: center;
}

button {
  background-color: #FFD700; /* 金黃色按鈕 */
  border: none;
  padding: 5px 10px;
  margin: 0 5px;
  cursor: pointer;
  font-size: 16px;
}

button:hover {
  background-color: #FFC300; /* 按鈕 hover 狀態 */
}

input[type="number"] {
  width: 60px;
  text-align: center;
}

.total {
  margin-top: 20px;
  font-weight: bold;
  color: #333;
}

button {
  background-color: #FFC300;
  color: #FFF;
  padding: 10px 20px;
  border-radius: 4px;
  cursor: pointer;
}

button:hover {
  background-color: #FFA500;
}


  </style>