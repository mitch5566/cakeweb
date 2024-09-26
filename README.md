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
>or  直接目錄內build
```bash
cd cakeweb 
dotnet build 
dotnet publish -c Release -r linux-x64 --self-contained -p:PublishSingleFile=true


```

>連接mysql 安裝這 包
```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design

//ef包

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
dotnet new class -n ApplicationDbContext -o Data

dotnet new class -n PaymentController -o Controllers
dotnet new class -n GreenWorldPaymentService -o Services

GreenWorldPaymentService



```
建立空白檔案

```bash
echo "" > 目錄  建立檔案

```


>view 專案打包後 放到wwwroot
```bash
xcopy dist\* ..\wwwroot\ /E /H /C /I
chmod +x /var/www/cakewe


sudo chmod  -R 777 cakeweb

```



>mysql指令

```bash
systemctl status nginx
sudo mysql -u root -p
sudo service mysql status

重新啟動mysql:
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
