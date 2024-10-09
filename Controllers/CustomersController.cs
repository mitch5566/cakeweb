// using System.IdentityModel.Tokens.Jwt;

// using System.Security.Claims;
// using System.Text;
// using Microsoft.AspNetCore.Authentication.Cookies;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.IdentityModel.Tokens;
// using test.Models;
// using Google.Apis.Auth;
// using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using cakeweb.Models;  // 替換成你的命名空間



namespace cakeweb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;


        //private readonly IConfiguration _configuration;

        public CustomersController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
           // _configuration = configuration;
        }

        // READ: 獲取所有客戶
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        // {
        //     return await _context.Customers.ToListAsync();
        // }

        // READ: 根據 ID 獲取單個客戶
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // CREATE: 新增客戶
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerId }, customer);
        }

        // UPDATE: 更新客戶資料
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: 刪除客戶
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }

        // 登入: 驗證帳號密碼
        [HttpPost("login")]
        public async Task<IActionResult> Login(Customer loginUser)
        {
            var user = await _context.Customers
                .FirstOrDefaultAsync(u => u.Account == loginUser.Account && u.Password == loginUser.Password);

            if (user == null)
            {
                return Unauthorized("帳號或密碼錯誤");
            }

            // 生成 JWT Token
            var token = GenerateJwtToken(user);

            // 创建 cookie
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Account)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // 这里的 cookie 选项可以根据需要进行调整
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // 持久化 cookie
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7) // 设置 cookie 过期时间
            };

            await HttpContext.SignInAsync("Cookies", claimsPrincipal, authProperties);

            return Ok(new { Token = token });
        }

        // 生成 JWT Token 方法
        private string GenerateJwtToken(Customer user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Account),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
