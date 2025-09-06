using System.Collections.Generic;
using System.Threading.Tasks;

using Product_Controller.Models;

namespace Product_Controller.Services
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
