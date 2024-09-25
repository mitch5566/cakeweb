using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace cakeweb;



public class GreenWorldPaymentService
{

     private readonly HttpClient _httpClient;

    public GreenWorldPaymentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<string> CreatePaymentRequestAsync(Dictionary<string, string> parameters)
    {
        // 測試使用 POST 請求到一個測試 API URL
        var requestUrl = "https://jsonplaceholder.typicode.com/posts"; // 測試 API
        //var requestUrl = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";

        // 模擬一些簡單的參數

// var parameters = new Dictionary<string, string>
// {
//     { "TradeDesc", "促銷方案" },
//     { "PaymentType", "aio" },
//     { "MerchantTradeDate", "2024/03/12 15:00:23" },
//     { "MerchantTradeNo", "ecpay20240925150023" },
//     { "MerchantID", "3002607" },
//     { "ReturnURL", "https://www.ecpay.com.tw/receive.php" },
//     { "ItemName", "Apple iphone 15" },
//     { "TotalAmount", "30000" },
//     { "ChoosePayment", "ALL" },
//     { "EncryptType", "1" }
// };
    
       //var sortedParameters = new SortedDictionary<string, string>(parameters);
        // 將字典按鍵名進行排序

        var sortedParameters = parameters.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value);


        var hashKey = "pwFHCqoQZGmho4w6";  // 測試環境的 HashKey
        var hashIV = "EkRm7iFT261dpevs";   // 測試環境的 HashIV

         // 最前面加入新的參數
        sortedParameters = new Dictionary<string, string>
        {
            { "HashKey", hashKey }
        }.Concat(sortedParameters).ToDictionary(p => p.Key, p => p.Value);
        // 最後面加入新的參數
        sortedParameters.Add("HashIV",hashIV);

        //都還是Dictionary
        
        // Step 2:    將鍵值對用 "&" 連接成字串    類似 new FormUrlEncodedContent(parameters);
        var concatenatedString = string.Join("&", sortedParameters.Select(p => $"{p.Key}={p.Value}"));
        
        var oriStr  = new FormUrlEncodedContent(parameters);//原始資料
       
        Console.WriteLine("原始 : " + oriStr);
        Console.WriteLine("檢查   的結果  MMMMMMMCSSSSSSSS: " + concatenatedString);

        //Step 3: 對整個字串進行 URL Encode
        var encodedContent = Uri.EscapeDataString(concatenatedString);
        
        // // 使用 string.Replace 直接替換 %20 
        // var replacedWithStringReplace =  encodedContent.Replace("%20", "+");
         // 使用正則表達式 Regex.Replace 僅精確替換 %20
        var replacedWithRegex = System.Text.RegularExpressions.Regex.Replace(encodedContent, "%20", "+");


        // 顯示結果
        //Console.WriteLine("使用 Regex.Replace 的結果: " + replacedWithRegex);

        // Step 4: 將字串轉換為小寫
        encodedContent = replacedWithRegex.ToLower();

        //Console.WriteLine("將字串轉換為小寫: " + encodedContent);

        // Step 5: 使用 SHA256 進行加密
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(encodedContent);
            byte[] hashBytes = sha256.ComputeHash(bytes);
            Console.WriteLine("雜湊值未轉換時vvv:" + BitConverter.ToString(hashBytes).Replace("-", ""));

            var sha256Hash = BitConverter.ToString(hashBytes).Replace("-", "");
            parameters.Add("CheckMacValue",sha256Hash);

            /// BitConverter.ToString已經為大寫
            //var sha256Hash = BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();
        }



            // 將參數轉換為表單格式
        var content = new FormUrlEncodedContent(parameters);

        // 發送 POST 請求
        var response = await _httpClient.PostAsync(requestUrl, content);




        // 檢查回應是否成功
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            return result;
            //return $"Sucess: {"ddddd"}";
        }
        else
        {
            return $"Error: {response.StatusCode}";
        }
    }

    internal async Task CreatePaymentRequestAsync()
    {
        throw new NotImplementedException();
    }

    private HttpContent? FormUrlEncodedContent(Dictionary<string, string> parameters)
    {
        throw new NotImplementedException();
    }

    // internal async Task CreatePaymentRequestAsync()
    // {
    //     throw new NotImplementedException();
    // }

    //     private string GenerateCheckMacValue(Dictionary<string, string> parameters, string hashKey, string hashIV)
    // {
    //     // 根據綠界的要求來生成 CheckMacValue
    //     // 詳細簽名算法請參考綠界的文件
    //     return "CheckMacValue";
    // }

    private void WriteLogToFile(string title, string message)
{
    string logFilePath = @"C:\Logs\payment_logs.txt"; // 設定你的檔案路徑
    string logEntry = $"{DateTime.Now}: {title}\n{message}\n";

    // 確保目錄存在
    var logDirectory = Path.GetDirectoryName(logFilePath);
    if (!Directory.Exists(logDirectory))
    {
        Directory.CreateDirectory(logDirectory);
    }

    // 將內容追加到檔案
    File.AppendAllText(logFilePath, logEntry);
}




}

