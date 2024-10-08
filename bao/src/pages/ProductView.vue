<template>
      
      <Header />

      <div>Hello sss<svg xmlns="http://www.w3.org/2000/svg" height="24" width="25.8" viewBox="0 0 24 24" fill="none">
  <circle cx="12" cy="6" r="5" stroke="currentColor" stroke-width="2"></circle>
  <path d="M17.607 23H6.393C5.54 23.04 4.705 22.744 4.069 22.173C3.433 21.602 3.049 20.802 3 19.947V19.117C3 14.608 7.032 11 12 11C16.968 11 21 14.608 21 19.117V19.947C20.951 20.802 20.567 21.602 19.931 22.173C19.295 22.744 18.46 23.04 17.607 23Z" stroke="currentColor" stroke-width="2" stroke-miterlimit="10"></path>
</svg>

<svg xmlns="http://www.w3.org/2000/svg" height="24" width="26" viewBox="0 0 24 24" fill="none">
  <path d="M21 11C21 16.523 16.523 21 11 21C5.477 21 1 16.523 1 11C1 5.477 5.477 1 11 1C16.523 1 21 5.477 21 11Z" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
  <path d="M19 19L23 23" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
</svg>

<svg xmlns="http://www.w3.org/2000/svg" height="24" width="32" viewBox="0 0 24 24" fill="none">
  <path d="M1 1H3.866L7.407 18H20" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
  <path d="M5 4H23L21.235 14.071L7.296 15" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
  <path d="M11.92 10.12H16.23" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
  <path d="M7 23C7.552 23 8 22.552 8 22C8 21.448 7.552 21 7 21C6.448 21 6 21.448 6 22C6 22.552 6.448 23 7 23Z" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
  <path d="M20 23C20.552 23 21 22.552 21 22C21 21.448 20.552 21 20 21C19.448 21 19 21.448 19 22C19 22.552 19.448 23 20 23Z" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
</svg>
<ProductList />



<Footer />


</div>
  

  </template>

  <script setup>
  import Header from '../components/Header.vue';
  import Footer from '../components/Footer.vue';
  import ProductList from '../components/ProductList.vue';


  </script>
  
  <style scoped>
  .logo {
      height: 6em;
      padding: 1.5em;
      will-change: filter;
      transition: filter 300ms;
    }
    .logo:hover {
      filter: drop-shadow(0 0 2em #646cffaa);
    }
    .logo.vue:hover {
      filter: drop-shadow(0 0 2em #42b883aa);
    }
    </style>