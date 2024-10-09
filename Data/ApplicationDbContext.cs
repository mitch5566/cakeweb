using Microsoft.EntityFrameworkCore;

namespace cakeweb;



public class ApplicationDbContext  : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public  DbSet<Customer> Customers { get; set; }

    public  DbSet<CustomersLogin> CustomersLogins { get; set; }

}
