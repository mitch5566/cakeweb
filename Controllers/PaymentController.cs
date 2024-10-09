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
       // 調用 TestPostRequestAsync 獲取 HttpResponseMessage
    var response = await _paymentService.TestPostRequestAsync(orderData);

    // 構建返回給前端的內容
    var content = await response.Content.ReadAsStringAsync();

    // 返回結果到前端，包括狀態碼、內容和標頭
    return new ContentResult
    {
        Content = content,
        StatusCode = (int)response.StatusCode,
        ContentType = response.Content.Headers.ContentType?.ToString() ?? "text/html"
    };
    

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