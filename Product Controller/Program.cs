using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Product_Controller.Data;
using Product_Controller.Features.Products.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Product_ControllerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Product_ControllerContext") ?? throw new InvalidOperationException("Connection string 'Product_ControllerContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews().
    AddRazorOptions(options => options.ViewLocationExpanders.Add(new Product_Controller.Infrastructure.FeatureViewLocationExpander()));

builder.Services.AddScoped<Product_Controller.Features.Products.Services.IProductService, Product_Controller.Features.Products.Services.ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
