using Microsoft.EntityFrameworkCore;
using TestVegaMVC.Models;

namespace TestVegaMVC
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Material>? Materials {get; set; }
    public DbSet<Supplier>? Supplier {get; set; }
  }
}