# Cakeweb Project Documentation v0.1

## 懶人指令

用於部署、發布專案到目標伺服器：

```bash
# 複製發佈檔案到指定伺服器
scp -r bin/Release/net8.0/linux-x64/publish/* mibaba1@192.168.36.129:/var/www/cakeweb 

# 發佈專案為自我包含的 Linux 可執行檔案
dotnet publish -c Release -r linux-x64 --self-contained -p:PublishSingleFile=true  
```


## 建立專案
```bash
# 建立 MVC 專案，並指定專案名稱與目錄
dotnet new mvc -n cakeweb -o cakeweb 
```

## build

```bash
# 指定專案檔案進行建置
dotnet build ./cakeweb/cakeweb.csproj
```
or 
```bash
cd cakeweb 
dotnet build 
dotnet publish -c Release -r linux-x64 --self-contained -p:PublishSingleFile=true

```

## 連接 MySQL 並安裝相關套件
```bash

# 安裝 Entity Framework Core 套件
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design

# 安裝 Pomelo MySQL EF Core Provider 及 EF Core CLI 工具
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet tool install --global dotnet-ef

```

<pre>
├── /Controllers          # 放置 MVC 控制器
│   └── PaymentController.cs
│
├── /Services             # 放置業務邏輯或與外部 API 互動的服務
│   └── GreenWorldPaymentService.cs
│
</pre>


```bash

# 建立 Data 資料夾並新增 ApplicationDbContext.cs
dotnet new class -n ApplicationDbContext -o Data

# 建立 Controllers 資料夾並新增 PaymentController.cs
dotnet new class -n PaymentController -o Controllers

# 建立 Services 資料夾並新增 GreenWorldPaymentService.cs
dotnet new class -n GreenWorldPaymentService -o Services


```
## 建立空白檔案

```bash
echo "" > 目錄  建立檔案

```


## view 專案打包後 放到wwwroot
```bash
# 複製打包後的檔案至 wwwroot
xcopy dist/* ../wwwroot/ /E /H /C /I

# 授權檔案可執行
chmod +x /var/www/cakeweb

# 調整目錄權限
sudo chmod -R 777 cakeweb
```



## mysql指令

```bash
# 檢查 nginx 服務狀態
systemctl status nginx

# 以 root 帳號登入 MySQL
sudo mysql -u root -p

# 查看 MySQL 服務狀態
sudo service mysql status

# 重新啟動 MySQL
systemctl restart mysql
```

```bash
curl -X POST http://localhost:5000/api/payment/CreatePayment -H "Content-Type: application/json" -d "{\"MerchantID\": \"3002607\", \"MerchantTradeNo\": \"TestOrder88252777\", \"MerchantTradeDate\": \"2024/09/25 12:34:56\", \"PaymentType\": \"aio\", \"TotalAmount\": \"1000\", \"TradeDesc\": \"Test Payment\", \"ItemName\": \"Sample Product\", \"ReturnURL\": \"https://yourdomain.com/api/payment/ReturnURL\", \"ChoosePayment\": \"ALL\", \"EncryptType\": \"1\"}"


```


```sql

CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY AUTO_INCREMENT,
    OrderID INT NOT NULL,
    PaymentDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    PaymentAmount DECIMAL(10, 2) NOT NULL,
    PaymentStatus VARCHAR(50) NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);
```


