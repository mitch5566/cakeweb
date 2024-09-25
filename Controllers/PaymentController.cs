namespace cakeweb;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]

public class PaymentController : ControllerBase
{

 private readonly GreenWorldPaymentService _paymentService;
private readonly ILogger<PaymentController> _logger;



        public PaymentController(ILogger<PaymentController> logger,GreenWorldPaymentService paymentService)
    {
        _logger = logger;
        _paymentService = paymentService;
    }

        // 接收前端的 POST 請求
    [HttpPost("CreatePayment")]
    public async Task<IActionResult> CreatePayment([FromBody] Dictionary<string, string> orderData)
    {
        if (orderData == null || orderData.Count == 0)
        {
            return BadRequest("Invalid order data");
        }

        // 寫死的字典，用來測試傳給 GreenWorldPaymentService
        var testParameters = new Dictionary<string, string>
        {
            { "MerchantID", "3002607" },
            { "MerchantTradeNo", "ecpay2023102515002" },
            { "MerchantTradeDate", "2024/09/25 12:34:56" },
            { "PaymentType", "aio" },
            { "TotalAmount", "1000" },
            { "TradeDesc", "Test Payment" },
            { "ItemName", "Sample Product" },
            { "ReturnURL", "https://yourdomain.com/api/payment/ReturnURL" },
            { "ChoosePayment", "ALL" },
            { "EncryptType", "1" }
        };

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

        // 調用 GreenWorldPaymentService，發送請求並獲取回應
        var response = await _paymentService.CreatePaymentRequestAsync(testParameters);

        // 返回綠界的回應結果給前端
        return Ok(new { response });
    }


//    // 注入 GreenWorldPaymentService
//     public PaymentController(GreenWorldPaymentService paymentService)
//     {
//         _paymentService = paymentService;
//     }

    // 發送支付請求的端點
    // [HttpPost("create")]
    // public async Task<IActionResult> CreatePayment()
    // {
    //     var result = await _paymentService.CreatePaymentRequestAsync();
    //     return Ok(result); // 返回支付請求的結果
    // }

       // 處理支付回調（ReturnURL）
    // [HttpPost("callback")]
    // public IActionResult PaymentCallback([FromForm] PaymentResponseModel response)
    // {
    //     if (response.RtnCode == "1") // RtnCode = 1 表示交易成功
    //     {
    //         // 更新訂單狀態，標記為已支付
    //         _logger.LogInformation($"Payment success for order {response.MerchantTradeNo}");

    //         // 根據商業邏輯處理，可能更新數據庫中的訂單狀態
    //     }
    //     else
    //     {
    //         // 處理支付失敗的情況
    //         _logger.LogWarning($"Payment failed for order {response.MerchantTradeNo}: {response.RtnMsg}");
    //     }

    //     // 綠介需要回傳 "OK" 來確認回調已成功接收
    //     return Content("OK");

    // }
}
