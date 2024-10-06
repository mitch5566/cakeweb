<template>
    <div class="calendar-container">
      <div class="calendar-header">
        <h1>{{ monthName }} {{ currentYear }}</h1>
        <span class="holiday-label">公休日</span>
      </div>
      <!-- 星期標題行 -->
      <div class="calendar-grid week-labels">
        <div v-for="day in weekDays" :key="day" class="week-day">{{ day }}</div>
      </div>
      <div class="calendar-grid">
        <div 
          v-for="day in daysOfMonth" 
          :key="day.date" 
          :class="{'selected': isSelected(day.date)}"
          @click="toggleHoliday(day.date)"
        >
          <span>{{ day.date }}</span>
          <span class="description">{{ getHolidayDescription(day.date) || ' ' }}</span>
        </div>
      </div>
      <div class="selected-holidays">
        <h3>公休日:</h3>
        <div class="holiday-list">
          <span v-for="holiday in holidays" :key="holiday" class="holiday-item">{{ holiday }}</span>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        currentYear: new Date().getFullYear(),
        currentMonth: new Date().getMonth(),
        holidays: ['2-國定', '10-國慶', '17'], // 公休日格式: "日期-描述" 或 "日期"
        weekDays: ['SUN', 'MON', 'TUE', 'WED', 'THU', 'FRI', 'SAT'] // 星期標題
      };
    },
    computed: {
      daysOfMonth() {
        const daysInMonth = new Date(this.currentYear, this.currentMonth + 1, 0).getDate();
        return Array.from({ length: daysInMonth }, (_, i) => ({
          date: i + 1
        }));
      },
      monthName() {
        const monthNames = [
          'January', 'February', 'March', 'April', 'May', 'June',
          'July', 'August', 'September', 'October', 'November', 'December'
        ];
        return monthNames[this.currentMonth];
      }
    },
    methods: {
      toggleHoliday(date) {
        const index = this.holidays.findIndex(holiday => holiday.startsWith(`${date}-`) || holiday === `${date}`);
        
        if (index > -1) {
          // 移除日期
          this.holidays.splice(index, 1);
        } else {
          // 添加新的日期，限制描述最多3字
          let description = prompt('請輸入描述 (最多三字):');
          if (description && description.length > 3) {
            description = description.slice(0, 3);
          }
          if (description) {
            this.holidays.push(`${date}-${description}`);
          } else {
            this.holidays.push(`${date}`);
          }
        }
      },
      getHolidayDescription(date) {
        const holiday = this.holidays.find(holiday => holiday.startsWith(`${date}-`));
        return holiday ? holiday.split('-')[1] : null;
      },
      isSelected(date) {
        return this.holidays.some(holiday => holiday.split('-')[0] === `${date}`);
        // return this.holidays.some(holiday => holiday.startsWith(`${date}`));
      }
    }
  };
  </script>
  
  <style scoped>
  /* 全局字體設置為手繪風格 */
  @import url('https://fonts.googleapis.com/css2?family=Indie+Flower&display=swap');
  
  .calendar-container {
    background-image: url('../assets/images/S__2547716_0.jpg'); /* 替換成你的圖片路徑 */
    background-size: cover;
    background-repeat: no-repeat;
    background-position: center;
    max-width: 600px;
    margin: auto;
    padding: 50px;
    color: #f5deb3; /* 字體顏色改為米色 */
    font-family: 'Indie Flower', cursive; /* 手繪字體 */
    text-align: center;
    border-radius: 10px;
  }
  
  .calendar-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    font-size: 2em;
    margin-top: 100px; /* 增加 margin 讓月份區域貼近日曆 */
  }
  
  .holiday-label {
    color: #b22222;
    font-size: 1.2em; /* 字體縮小 */
  }
  
  .calendar-grid {
    display: grid;
    grid-template-columns: repeat(7, 1fr);
    gap: 10px;
    margin-top: 10px; /* 星期標題和日曆的間距 */
  }
  
  /* 星期標題樣式 */
  .week-labels .week-day {
    font-weight: bold;
    font-size: 1.2em;
  }
  
  /* 日曆格子樣式 */
  .calendar-grid div {
    width: 50px; /* 固定寬度 */
    height: 50px; /* 固定高度，保持圓形 */
    border-radius: 50%; /* 圓形 */
    transition: color 0.3s;
    cursor: pointer;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center; /* 垂直置中 */
    overflow: hidden;
  }
  
  /* 滑鼠懸停時文字顏色變化 */
  .calendar-grid div:hover {
    color: #ff6347;
  }
  
  /* 被選中的日期文字顏色為紅色 */
  .calendar-grid .selected { 
  color: #b22222; /* 被選中的日期文字顏色為紅色 */ 
}

  /* 描述樣式 */
  .calendar-grid .description {
    margin-top: 3px; /* 第二行的空白間距 */
    font-size: 0.8em;
  }
  
  /* 公休日區域 */
  .selected-holidays {
    margin-top: 20px;
    color: #f5deb3; /* 字體顏色改為米色 */
  }
  
  /* 公休日列表 */
  .holiday-list {
    display: flex;
    flex-wrap: wrap;
    gap: 10px;
    justify-content: center;
    margin-top: 10px;
  }
  
  .holiday-item {
    background-color: rgba(255, 0, 0, 0.7);
    border-radius: 5px;
    padding: 5px 10px;
    color: #fff;
  }
  </style>