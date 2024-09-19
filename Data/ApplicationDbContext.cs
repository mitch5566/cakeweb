using Microsoft.EntityFrameworkCore;

namespace cakeweb;

public class ApplicationDbContext  : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

}
