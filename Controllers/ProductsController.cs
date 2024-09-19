namespace cakeweb;


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; //ToListAsync
//using System.Linq;
using System.Threading.Tasks;


[ApiController]
[Route("api/[controller]")]

public class ProductsController  : ControllerBase
{

    private readonly ApplicationDbContext _context;

     public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _context.Products.ToListAsync();

        return Ok(new
        {
            code = 200,
            message = "Products retrieved successfully",
            table = "Products",
            data = products
        });
    }



}
