<template>
  <div id="home">
    <Header />
    <!-- 這裡是 Section 和 MainComponent 並排顯示的區塊 -->
    <div class="content-layout">
     <!-- <Article /> -->
     <Section />
      <Main />
    </div>

    <!-- CarouselComponent 單獨出來的區塊 -->
    <div class="carousel-layout">
      <Carousel />
    </div>

    <!-- ProductGridComponent 插入到頁面中 -->
    <div class="product-grid-layout">
      <ProductGridComponent />
    </div>


    <!-- CalendarComponent 插入到頁面中 -->
    <div class="calendar-layout">
      <Calendar />
    </div>

    <!-- Instagram 貼文輪播區塊 -->
    <div class="instagram-carousel-layout">
      <Instagram />
    </div>
    <Footer />
    

  </div>

    <!-- <HelloWorld msg="轟轟轟" /> -->
 
</template>

<script setup>

import Carousel from '../components/Carousel.vue';
import Main from '../components/Main.vue';
import Section from '../components/Section.vue';
import Header from '../components/Header.vue';
import Calendar from '../components/Calendar.vue';
//import HelloWorld from '../components/HelloWorld.vue'
import Instagram from '../components/Instagram.vue';
import Footer from '../components/Footer.vue';



</script>

<style scoped>
/* 設置背景圖片 */


/* 設置主要佈局 */
.content-layout {
  display: flex;
  justify-content: space-between; /* 讓SectionComponent和MainComponent並排 */
  padding: 2rem;
  gap: 1rem; /* 兩個區塊之間的間距 */
}

/* CarouselComponent 獨立的樣式 */
.carousel-layout {
  margin-top: 2rem; /* CarouselComponent 與上面的區塊保持間距 */
  padding: 2rem;
  text-align: center; /* 讓 CarouselComponent 居中對齊 */
}

/* ProductGridComponent 獨立的樣式 */
.product-grid-layout {
  margin-top: 2rem;
  padding: 2rem;
  text-align: center; /* 讓 ProductGridComponent 居中對齊 */
}

/* CalendarComponent 獨立的樣式 */
.calendar-layout {
  margin-top: 2rem;
  padding: 2rem;
  text-align: center; /* 讓 CalendarComponent 居中對齊 */
}

/* Instagram 貼文輪播的樣式 */
.instagram-carousel-layout {
  margin-top: 2rem;
  padding: 2rem;
  text-align: center; /* 讓 InstagramCarousel 居中對齊 */
}

@media (max-width: 768px) {
  .content-layout {
    flex-direction: column; /* 小螢幕下切換為垂直佈局 */
    padding: 1rem;
  }

  .carousel-layout,
  .product-grid-layout,
  .calendar-layout,
  .instagram-carousel-layout {
    padding: 1rem;
  }
}
  </style>