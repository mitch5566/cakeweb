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

    public async Task<HttpResponseMessage> TestPostRequestAsync (Dictionary<string, string> parameters)
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

            // 處理需要格式化的字段
    foreach (var key in parameters.Keys.ToList()) // 使用 ToList 防止修改字典時出錯
    {
        // 根據字段名進行格式化

            switch (key)
        {
            case "MerchantID":
                // 修正 MerchantID 為固定長度 10，不能為空
                parameters[key] = parameters[key].ToString().Length > 10
                    ? parameters[key].ToString().Substring(0, 10)
                    : parameters[key].ToString().PadRight(10, ' ');
                break;

            case "MerchantTradeNo":
                // 修正 MerchantTradeNo 為最多長度 20，不能為空
                parameters[key] = parameters[key].ToString().Length > 20
                    ? parameters[key].ToString().Substring(0, 20)
                    : parameters[key].ToString();
                break;

            case "MerchantTradeDate":
                // MerchantTradeDate 必須是 yyyy/MM/dd HH:mm:ss 格式
                DateTime tradeDate;
                if (DateTime.TryParse(parameters[key].ToString(), out tradeDate))
                {
                    parameters[key] = tradeDate.ToString("yyyy/MM/dd HH:mm:ss");
                }
                break;

            case "PaymentType":
                // PaymentType 為固定值 "aio"
                parameters[key] = "aio";
                break;

            case "TotalAmount":
                // TotalAmount 為數字，確保格式正確
                if (int.TryParse(parameters[key].ToString(), out int amount))
                {
                    parameters[key] = amount.ToString();
                }
                break;

            case "TradeDesc":
                // TradeDesc 為長度最多 200 的字串
                parameters[key] = parameters[key].ToString().Length > 200
                    ? parameters[key].ToString().Substring(0, 200)
                    : parameters[key].ToString();
                break;

            case "ItemName":
                // ItemName 長度最多 400，中間用 # 分隔多個項目
                parameters[key] = parameters[key].ToString().Length > 400
                    ? parameters[key].ToString().Substring(0, 400)
                    : parameters[key].ToString();
                break;

            case "ReturnURL":
                // ReturnURL 為長度最多 200 的字串
                parameters[key] = parameters[key].ToString().Length > 200
                    ? parameters[key].ToString().Substring(0, 200)
                    : parameters[key].ToString();
                break;

            case "ChoosePayment":
                // ChoosePayment 為支付類型，確保是 ECPay 支持的支付方式之一
                // 這裡假設 ChoosePayment 不為空
                parameters[key] = parameters[key].ToString();
                break;

            case "EncryptType":
                // EncryptType 為 1，固定值
                parameters[key] = "1";
                break;

            // 其他需要處理的字段
            // case "SomeOtherField":
            //     parameters[key] = ... // 處理方式
            //     break;

            default:
                // 可選：對於其他字段可以根據需要進行處理
                break;
        }
            // 添加其他需要特殊格式處理的字段
        }
    

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

//  post 前 先遍立一次

        foreach (var param in parameters)
            {
            Console.WriteLine($"{param.Key}: {param.Value}");
            }


        // 發送 POST 請求
        var response = await _httpClient.PostAsync(requestUrl, form1);

        // 檢查回應是否成功
        if (response.IsSuccessStatusCode)
        {
            //var result = await response.Content.ReadAsStringAsync();

            return response;
        //    return Tuple.Create(true, result); 
           // 成功時返回 Tuple // 成功時返回 true 和內容

        }
        else
        {
            return response; // 成功時返回 Tuple$"Error: {response.StatusCode}";
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

