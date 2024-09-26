# cakeweb
 v01


##

>懶人
```bash

scp -r  bin\Release\net8.0\linux-x64\publish\*  mibaba1@192.168.36.129:/var/www/cakeweb 
dotnet publish -c Release -r linux-x64 --self-contained -p:PublishSingleFile=true  

 
建立專案
```bash
dotnet new mvc  -n cakeweb  -o cakeweb 

```

build
```bash
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


├── /Controllers          # 放置 MVC 控制器
│   └── PaymentController.cs
│
├── /Services             # 放置業務邏輯或與外部 API 互動的服務
│   └── GreenWorldPaymentService.cs
│


<pre> ├── /Controllers # 放置 MVC 控制器 │ └── PaymentController.cs ├── /Services # 放置業務邏輯或與外部 API 互動的服務 │ └── GreenWorldPaymentService.cs </pre>


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
