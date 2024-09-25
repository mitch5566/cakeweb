using cakeweb;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//設定 Kestrel 綁定到所有 IP 地址（包括外部）
builder.WebHost.UseUrls("http://0.0.0.0:5000");

// // Add services to the container.
// builder.Services.AddControllersWithViews();

// 註冊 GreenWorldPaymentService
builder.Services.AddTransient<GreenWorldPaymentService>();



        // // 初始化 HttpClient
        // var httpClient = new HttpClient();

        // // 初始化你的 GreenWorldPaymentService
        // var paymentService = new GreenWorldPaymentService(httpClient);

        // // 執行測試的 POST 請求
        // var result = await paymentService.CreatePaymentRequestAsync();

        // // 打印結果
        // Console.WriteLine(result);



// 加入服務，並設定 MySQL 的連線字串
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 26))));

builder.Services.AddControllers();  // 註冊 MVC 控制器服務

// 註冊 HttpClient
builder.Services.AddHttpClient(); // 確保這行已添加

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
}); //跨域存取for vue vite



var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("AllowAllOrigins"); //上面跨域

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();



// 處理 SPA 路由 (針對 Vue.js 的前端路由)
app.MapFallbackToFile("index.html");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// app.MapControllers();

app.Run();
