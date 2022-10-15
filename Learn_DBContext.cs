using Microsoft.EntityFrameworkCore;
using product_service.Entity;
using System.Security.Principal;

public class Learn_DBContext: DbContext
{
    public Learn_DBContext(DbContextOptions<Learn_DBContext> options) : base(options)
    {
        Database.EnsureCreated();
    }



    public DbSet<products> products { get; set; } = null;
    public DbSet<Users> users { get; set; } = null;

}