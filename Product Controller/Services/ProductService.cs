using Microsoft.EntityFrameworkCore;
using Product_Controller.Data;
using Product_Controller.Models;

namespace Product_Controller.Services
{
    public class ProductService : IProductService
    {

        private readonly Product_ControllerContext _db;
          public ProductService(Product_ControllerContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() => await _db.Product.ToListAsync(); 

        public async Task<Product?> GetByIdAsync(int id) => await _db.Product.FirstOrDefaultAsync(product => product.Id == id);
   
        public async Task AddAsync(Product product)
        {
            _db.Product.Add(product);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _db.Product.Update(product);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Product.FindAsync(id);

            if (entity is null) return;
            
            _db.Product.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
