using System.Collections.Generic;
using System.Threading.Tasks;
using Product_Controller.Features.Products.Models;

namespace Product_Controller.Features.Products.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);

        Task AddAsync(Product product);
        Task UpdateAsync(Product product);

        Task DeleteAsync(int id);
    }
}
