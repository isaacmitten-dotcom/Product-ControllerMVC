using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product_Controller.Data;
using Product_Controller.Models;
using Product_Controller.Services;
using Xunit;


namespace Tests;

public class ProductService_CRUDTest
{
    private static ProductService CreateService()
    {
        var dbCtxBuilder = new DbContextOptionsBuilder<Product_ControllerContext>();
        var inMemDb = dbCtxBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        var options = inMemDb.Options;
        var context = new Product_ControllerContext(options);

        return new ProductService(context);

    }

    [Fact]
    public async Task ProductService_CanPerformCRUDOp()
    {
       var svc = CreateService();
        
        //Create
        var product = new Product
        {
            Name = "Sickle",
            Price = 0.8M
        };

        await svc.AddAsync(product);
        
        //Read
        var read = await svc.GetByIdAsync(product.Id);
        Assert.NotNull(read);
        Assert.Equal(product.Name, read.Name);


        //Update
        read.Name = "Trowel";
        await svc.UpdateAsync(product);

        var updated = await svc.GetByIdAsync(product.Id);
        Assert.NotNull(updated);
        Assert.Equal(read.Name, updated.Name);


        //Delete
        await svc.DeleteAsync(product.Id);
        var deleted = await svc.GetByIdAsync(product.Id);
        Assert.Null(deleted);
    }
}
