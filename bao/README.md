# 爆的vue前端

cs http requeset 
body


```bash
npm create vite@latest bao

cd bao
yarn install




```

vite.config.ts加入 
```js
export default defineConfig({
  plugins: [vue()],
  server: {
    proxy: {
      '/api': 'http://localhost:5000', // 將 API 請求代理到後端 .NET 服務
    }
  }
})
```

```bash
mkdir src\pages
echo ^<template^> ^<div^> ^<h1^>Home Page^</h1^> ^<p^>Welcome to the Home Page!^</p^> ^</div^> ^</template^> ^<script setup lang='ts'^> ^</script^> ^<style scoped^> h1 { color: blue; } ^</style^> > src\pages\HomeView.vue

mkdir src\router
echo `` > src\router\index.ts


# types的 vue-router
yarn add @types/vue-router -D  



```









This template should help get you started developing with Vue 3 and TypeScript in Vite. The template uses Vue 3 `<script setup>` SFCs, check out the [script setup docs](https://v3.vuejs.org/api/sfc-script-setup.html#sfc-script-setup) to learn more.

Learn more about the recommended Project Setup and IDE Support in the [Vue Docs TypeScript Guide](https://vuejs.org/guide/typescript/overview.html#project-setup).
