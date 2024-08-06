// using System.IO;

// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Design;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using XXXXX.Context.Kernel;

// namespace XXXXX.Context.Migrations
// {
//     public class CustomApplicationContextDesignFactory : IDesignTimeDbContextFactory<CustomApplicationContext>
//     {
//         public CustomApplicationContext CreateDbContext(string[] args)
//         {
//             var provider = Program.ServiceProvider;

//             // Create our DbContext.
//             return provider.GetRequiredService<CustomApplicationContext>();
//         }
//     }
// }