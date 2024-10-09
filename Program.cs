using cakeweb;
using Microsoft.EntityFrameworkCore;
//先不注入
//using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

//設定 Kestrel 綁定到所有 IP 地址（包括外部）

//builder.WebHost.UseUrls("http://0.0.0.0:5000");

// // Add services to the container.
// builder.Services.AddControllersWithViews();

// 設定 Kestrel 綁定到所有 IP 地址
builder.WebHost.UseKestrel()
    .UseUrls("http://0.0.0.0:5000"); // 讓應用程式監聽所有網卡上的5000端口



// 註冊 GreenWorldPaymentService
// builder.Services.AddTransient<GreenWorldPaymentService>();
builder.Services.AddHttpClient<GreenWorldPaymentService>();

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


// Add Google authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = GoogleDefaults.AuthenticationScheme;
}).AddGoogle(options =>
{
    options.ClientId = builder.Configuration["AuthenticationBuilder:Google:ClientId"];
    options.ClientSecret = builder.Configuration["AuthenticationBuilder:Google:ClientSecret"]; // 若有使用 ClientSecret
});
//不知道
builder.Services.AddControllers();




var app = builder.Build();

// 處理 SPA 路由 (針對 Vue.js 的前端路由)
app.MapFallbackToFile("index.html"); // 這會確保 SPA 路由被正確處理，並轉發到 index.html



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseCors("AllowAllOrigins"); //上面跨域




// 移除或注釋掉這行代碼以禁用 HTTPS 重定向
//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
// 查誰是這個請求的發送者（用戶是誰）通過 Cookie、JWT Token、OAuth等）

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"  
    );

// app.MapControllers();

app.Run();
