# cakeweb
 v01


##

>懶人
```bash

scp -r  bin\Release\net8.0\linux-x64\publish\*  mibaba1@192.168.36.129:/var/www/cakeweb 
dotnet publish -c Release -r linux-x64 --self-contained -p:PublishSingleFile=true  


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