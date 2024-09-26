using System.Security.Cryptography;
using System.Text;

namespace cakeweb;


public class GreenWorldPaymentService
{

 private readonly HttpClient _httpClient;

    public GreenWorldPaymentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> TestPostRequestAsync (Dictionary<string, string> parameters)
    {
        // 測試使用 POST 請求到一個測試 API URL
        //var requestUrl = "https://jsonplaceholder.typicode.com/posts"; // 測試 API

        // 根據綠介測試環境修改此URL
        var requestUrl = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";

        //模擬一些簡單的參數

        // var parameters = new Dictionary<string, string>
        // {
        //     { "TradeDesc", "促銷方案" },
        //     { "PaymentType", "aio" },
        //     { "MerchantTradeDate", "2023/03/12 15:30:23" },
        //     { "MerchantTradeNo", "ecpay20230312153023" },
        //     { "MerchantID", "3002607" },
        //     { "ReturnURL", "https://www.ecpay.com.tw/receive.php" },
        //     { "ItemName", "Apple iphone 15" },
        //     { "TotalAmount", "30000" },
        //     { "ChoosePayment", "ALL" },
        //     { "EncryptType", "1" }
        // };


        // 使用 SortedDictionary 按鍵名排序
        var sortedParameters = parameters.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value);

        var hashKey = "pwFHCqoQZGmho4w6";  // 測試環境的 HashKey
        var hashIV = "EkRm7iFT261dpevs";   // 測試環境的 HashIV

            //參數最前面加上HashKey、最後面加上HashIV 
            // 3. 在排序好的參數前面加上 HashKey 和最後加上 HashIV

                // 最後面加入新的參數
                //  {"HashKey", hashKey}.Add(sortedParameters);

            sortedParameters = new Dictionary<string, string>
                {
                    { "HashKey", hashKey }
                }.Concat(sortedParameters).ToDictionary(p => p.Key, p => p.Value);

                sortedParameters.Add("HashIV", hashIV);

        // 將參數轉換為表單格式1 因為要做轉小寫 先不用FormUrlEncodedContent
        //var content1 = new FormUrlEncodedContent(sortedParameters);
        // Step 1: 將鍵值對拼接成字串
        var content = string.Join("&", sortedParameters.Select(p => $"{p.Key}={p.Value}"));
        // Step 2: 對整個拼接的字串進行 URL Encode
        String encodedContent = Uri.EscapeDataString(content);
        // Step 3: 使用正則表達式只替換 "%20" 為 "+"
        encodedContent = System.Text.RegularExpressions.Regex.Replace(encodedContent, "%20", "+");


        // Step 4: 將字串轉換為小寫
        encodedContent = encodedContent.ToLower();

        // Step 5: 使用 SHA256 進行加密
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(encodedContent);
            byte[] hashBytes = sha256.ComputeHash(bytes);

            // Step 6: 將加密後的結果轉換為大寫的十六進制字串
            var sha256Hash = BitConverter.ToString(hashBytes).Replace("-", "");  // 不需要再轉大寫          
           
            // 顯示結果
            //Console.WriteLine("SHA256 大寫加密後的結果: " + sha256Hash);

            //原始字典加入檢查碼
            parameters.Add("CheckMacValue", sha256Hash);

        }
        //毛很多要post就可以 FormUrlEncodedContent 


        var form1 = new FormUrlEncodedContent(parameters);


        // 發送 POST 請求
        var response = await _httpClient.PostAsync(requestUrl, form1);

        // 檢查回應是否成功
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            return result;

        }
        else
        {
            return $"Error: {response.StatusCode}";
        }

    }




    //     private string GenerateCheckMacValue(Dictionary<string, string> parameters, string hashKey, string hashIV)
    // {
    //     // 根據綠界的要求來生成 CheckMacValue
    //     // 詳細簽名算法請參考綠界的文件
    //     return "CheckMacValue";
    // }

//     private void WriteLogToFile(string title, string message)
// {
//     string logFilePath = @"C:\Logs\payment_logs.txt"; // 設定你的檔案路徑
//     string logEntry = $"{DateTime.Now}: {title}\n{message}\n";

//     // 確保目錄存在
//     var logDirectory = Path.GetDirectoryName(logFilePath);
//     if (!Directory.Exists(logDirectory))
//     {
//         Directory.CreateDirectory(logDirectory);
//     }

//     // 將內容追加到檔案
//     File.AppendAllText(logFilePath, logEntry);
// }




}

