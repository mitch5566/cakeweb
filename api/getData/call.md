### API  部署與配置筆記 php

`https://developers.ecpay.com.tw/?p=9058`


#### 使用 `scp` 進行檔案傳輸
將 `index.php` 和 `getData.php` 傳輸到目標伺服器：
 


```bash
scp api/getData/index.php mibaba1@192.168.118.128:/tmp/
scp api/getData.php mibaba1@192.168.118.128:/tmp/
```

#### 啟動 PHP 本地伺服器
在 localhost:8000 啟動 PHP 伺服器：
```
php -S localhost:8000
```


使用 curl 測試 API 端點
測試 POST 請求：

```bash

curl -X POST http://localhost:8000

curl -X POST http://localhost:8000/getData.php -H "Content-Type: application/json" -d "{\"RtnCode\": 1, \"RtnMsg\": \"Success\", \"MerchantID\": \"3002607\", \"OrderInfo\": {\"MerchantTradeNo\": \"20180914001\", \"TradeNo\": \"1809261503338172\", \"TradeDate\": \"2018/09/26 14:59:54\"}, \"CardInfo\": {\"Gwsr\": 10735183, \"ProcessDate\": \"2018/09/26 14:59:54\", \"AuthCode\": \"777777\", \"Amount\": 100, \"Eci\": 2, \"Card4No\": \"2222\", \"Card6No\": \"491122\", \"RedDan\": 0, \"RedOkAmt\": 0, \"RedYet\": 0}}"
```



### API 部署與配置筆記

#### 使用 `scp` 進行檔案傳輸
將 `index.php` 和 `getData.php` 傳輸到目標伺服器：

```bash
scp api/getData/index.php mibaba1@192.168.118.128:/tmp/
scp api/getData.php mibaba1@192.168.118.128:/tmp/


```


### 啟動 PHP 本地伺服器
在 localhost:8000 啟動 PHP 伺服器：

```bash
php -S localhost:8000
```

### 使用 curl 測試 API 端點
測試 POST 請求：

```bash

curl -X POST http://localhost:8000

curl -X POST http://localhost:8000/getData.php -H "Content-Type: application/json" -d "{\"RtnCode\": 1, \"RtnMsg\": \"Success\", \"MerchantID\": \"3002607\", \"OrderInfo\": {\"MerchantTradeNo\": \"20180914001\", \"TradeNo\": \"1809261503338172\", \"TradeDate\": \"2018/09/26 14:59:54\"}, \"CardInfo\": {\"Gwsr\": 10735183, \"ProcessDate\": \"2018/09/26 14:59:54\", \"AuthCode\": \"777777\", \"Amount\": 100, \"Eci\": 2, \"Card4No\": \"2222\", \"Card6No\": \"491122\", \"RedDan\": 0, \"RedOkAmt\": 0, \"RedYet\": 0}}"
```


### 配置 Nginx 伺服器
檢查 Nginx 配置並重新啟動服務：

```bash
sudo nginx -t
sudo systemctl restart nginx
sudo systemctl restart php8.3-fpm
```

### 安裝 PHP 和 PHP-FPM
更新並安裝 php-fpm 和 php-mysql：

```bash
sudo apt update
sudo apt install php-fpm php-mysql
```

### 編輯 Nginx 配置
編輯 /etc/nginx/sites-available/default：

sudo vim /etc/nginx/sites-available/default

```nginx
location ~ \.php$ {
          include snippets/fastcgi-php.conf;
  #
  #       # With php-fpm (or other unix sockets):
          fastcgi_pass unix:/run/php/php8.3-fpm.sock;
  #       # With php-cgi (or other tcp sockets):
  #       # fastcgi_pass 127.0.0.1:9000;
  }
```


```nginx
location /api/getData {
    index index.php;
}
```



在 Nginx 伺服器上測試 API 端點
測試對已部署的 Nginx 伺服器發送 POST 請求：


curl -X POST http://192.168.118.128:80/api/getData/
