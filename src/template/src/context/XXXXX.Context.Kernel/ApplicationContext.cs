using Foundation.Extension.Context;
using Microsoft.EntityFrameworkCore;

namespace XXXXX.Context.Kernel
{
  public class ApplicationContext : BaseApplicationContext
  {
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }
  }
}