import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
//import path from 'path';

export default defineConfig({
  plugins: [vue()],
  server: {
    proxy: {
      '/api': 'http://localhost:5000', // 將 API 請求代理到後端 .NET 服務
    }
  }
})