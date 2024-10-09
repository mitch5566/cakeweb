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
        <svg class="cart-svg qk-text--nav_menu_icon qk-vert--mid" xmlns="http://www.w3.org/2000/svg" height="24" width="32" viewBox="0 0 24 24" fill="none">
<path d="M1 1H3.86592L7.40731 18H20" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
<path d="M5 4H23L21.2347 14.0711L7.29582 15" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
<path d="M11.9199 10.1201H16.2299" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
<path d="M7 23C7.55228 23 8 22.5523 8 22C8 21.4477 7.55228 21 7 21C6.44772 21 6 21.4477 6 22C6 22.5523 6.44772 23 7 23Z" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
<path d="M20 23C20.5523 23 21 22.5523 21 22C21 21.4477 20.5523 21 20 21C19.4477 21 19 21.4477 19 22C19 22.5523 19.4477 23 20 23Z" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
</svg>加載中...
      </div>
  
      <div class="total" v-if="allProducts.length > 0">
      <span>總購買數量: {{ totalPurchaseQuantity }}</span>
      <span>總價: {{ totalPrice }} 元</span> <!-- 添加總價顯示 -->
    </div>
  
      <button @click="submitOrder" v-if="allProducts.length > 0">送出訂單</button>
    </div>
  </template>
  
  <script setup>
  //import axios from 'axios';
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
  const submitOrder = async () => {
    const order = allProducts.value
      .filter(product => product.purchaseQuantity > 0)
      .map(product => ({
        ID: product.productID,
        n: product.productName,
        q: product.purchaseQuantity
      }));
  
   // 使用 alert 提示購買數量
    alert(JSON.stringify(order, null, 2));
    alert('怕豹 來囉 ');

    const orderPayload = {
    MerchantID: "3002607",
    // MerchantTradeNo: "TestOrder" + Date.now(),
    MerchantTradeNo: "Cakephp" + Date.now(),
    // MerchantTradeDate: new Date().toISOString(),
    MerchantTradeDate: new Date().toLocaleString('sv'),
    PaymentType: "aio",
    TotalAmount: (totalPrice.value).toString(),

    TradeDesc: "Test Payment",
    // ItemName: order.map( p =>`${p.n}xOx${p.q}`).join("#").toString(),
    ItemName: "qqqqqq",
    ReturnURL: "http://34.168.66.212:80",
    // ReturnURL: "https://yourdomain.com/api/payment/",
    ChoosePayment: "ALL",
    EncryptType: (1).toString()
  };


  // https://jsonplaceholder.typicode.com/posts
  // http://localhost:5000/api/payment/CreatePayment


  
  alert(JSON.stringify(orderPayload));
  try {
    const response = await fetch("/api/payment/CreatePayment", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(orderPayload)
    });

    
      // 檢查回應狀態
      if (response.ok) {
      // 如果狀態碼是 200，則表示是 ContentResult
    console.log(response);

    // 如果你想要顯示整個 response 的內容和標頭
    const headers = [...response.headers.entries()].map(([key, value]) => `${key}: ${value}`).join("\n");
    const status = `Status: ${response.status} ${response.statusText}`;

    alert(`${status}\n\nHeaders:\n${headers}`);

    const htmlContent = await response.text();
    // newWindow.document.write(htmlContent);

    const newWindow = window.open();
    newWindow.document.open();
    newWindow.document.write(htmlContent);
    newWindow.document.close();


    } else {
      // 如果狀態碼不是 200，表示是 BadRequest，獲取錯誤信息
      const errorContent = await response.json();

      // 顯示錯誤信息
      alert(`錯誤碼: ${response.status}\n錯誤信息: ${errorContent.Message}\n詳細內容: ${errorContent.Details}`);
    }
  // if (contentType && contentType.includes("application/json")) {
  //   // 如果是 JSON 格式
  //   result = await response.json();

} catch (error) {
  console.error("Error submitting order:", error);
  alert("Failed to submit order.");
}

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