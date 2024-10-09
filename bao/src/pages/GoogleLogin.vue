<template>
    <div>
      <GoogleLogin
        clientId="YOUR_GOOGLE_CLIENT_ID"
        @success="onSuccess"
        @error="onError"
      >
        <button>Login with Google</button>
      </GoogleLogin>
    </div>
  </template>
  
  <script>
  import { GoogleLogin } from 'vue3-google-login'
  
  export default {
    components: { GoogleLogin },
    methods: {
      onSuccess(response) {
        console.log('Login success:', response);
        // 處理成功登入後的邏輯
      },
      onError(error) {
        console.log('Login error:', error);
        // 處理登入錯誤
      }
    }
  }
  </script>