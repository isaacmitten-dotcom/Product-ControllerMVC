using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Product_Controller.Data;
using System;
using System.Linq;


namespace Product_Controller.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Product_ControllerContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<Product_ControllerContext>>()))
            {
                
                if (context.Product.Any())
                {
                    return;   // DB has been seeded
                }
                context.Product.AddRange(
                    new Product { Name = "Hammer", Price = 6 },
                    new Product { Name = "Screw Driver", Price = 2 },
                    new Product { Name = "Drill", Price = 60 },
                    new Product { Name = "Pvc Pipe", Price = 20 }
                );
               
                context.SaveChanges();
            }
        }
    }
}