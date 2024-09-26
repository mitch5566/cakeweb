using Microsoft.AspNetCore.Mvc;

namespace cakeweb;


[ApiController]
[Route("api/[controller]")]

public class PaymentController : ControllerBase
{

    private readonly GreenWorldPaymentService _paymentService;

    //注入 GreenWorldPaymentService
    public PaymentController(GreenWorldPaymentService paymentService)
    {
        _paymentService = paymentService;
    }


    // 接收前端的 POST 請求
    [HttpPost("CreatePayment")]
    public async Task<IActionResult> CreatePayment([FromBody] Dictionary<string, string> orderData)
    {
          // 記錄接收到的請求
        //Console.WriteLine($"Received request: {JsonConvert.SerializeObject(orderData)}");
        // Dictionary<string, string> orderData = new Dictionary<string, string>();

        // // 添加額外的必需參數
        // orderData.Add("MerchantID", "3002607");
        // orderData.Add("MerchantTradeDate", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        // orderData.Add("PaymentType", "aio");
        // orderData.Add("ReturnURL", "https://yourdomain.com/api/payment/ReturnURL"); // 後端 ReturnURL
        // orderData.Add("ChoosePayment", "ALL");
        // orderData.Add("EncryptType", "1");

    // 調用 GreenWorldPaymentService 來處理支付請求


    var result = await _paymentService.TestPostRequestAsync(orderData);

    // 回應支付頁面給前端
            
            
            //Console.WriteLine("看看裡面有什麼"+result);

    if (result.Item1) // 布林值在 Item1
    {
        return new ContentResult
        {
            Content = result.Item2, // 內容在 Item2
            ContentType = "text/html"
        };
    }
    else
    {
        return BadRequest(new
        {
            Code = 400,
            Message = "訂單失敗",
            Details = result.Item2
        });
    }

          // return Content(result, "application/json");

    }


    [HttpPost("ReturnURL")]
    public IActionResult ReceivePaymentResult([FromForm] Dictionary<string, string> paymentResult)
    {
        // 這裡處理綠界回傳的支付結果
        if (paymentResult != null && paymentResult.Count > 0)
        {
            var status = paymentResult["RtnCode"]; // 支付結果代碼
            if (status == "1")  // 1 表示成功
            {
                // 交易成功邏輯，更新訂單狀態
                return Ok("Payment Success");
            }
            else
            {
                // 交易失敗邏輯
                return BadRequest("Payment Failed");
            }
        }
        return BadRequest("Invalid Response");
    }

}