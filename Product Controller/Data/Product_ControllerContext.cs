using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product_Controller.Models;

namespace Product_Controller.Data
{
    public class Product_ControllerContext : DbContext
    {
        public Product_ControllerContext (DbContextOptions<Product_ControllerContext> options)
            : base(options)
        {
        }

        public DbSet<Product_Controller.Models.Product> Product { get; set; } = default!;
    }
}
