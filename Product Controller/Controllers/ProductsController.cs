using Microsoft.AspNetCore.Mvc;
using Product_Controller.Models;
using Microsoft.Extensions.Logging;
using Product_Controller.Services;

namespace Product_Controller.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _products;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService products, ILogger<ProductsController> logger)
        {
            _products = products;
            _logger = logger;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchString)
        {

            IEnumerable<Product> all = await _products.GetAllAsync();
            IEnumerable<Product> products = all;


            if (!string.IsNullOrEmpty(searchString))
            {
                _logger.LogWarning("Search string provided: {SearchString}", searchString);
                products = products.Where(s => s.Name != null &&
                                            s.Name!.ToUpper().Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            var ProductSearchVM = new ProductSearchViewModel
            {
                Products = products.ToList(),
            };

            return View(ProductSearchVM);
        }


        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var product = _products.GetByIdAsync(id);

            _logger.LogInformation("GET details for Id:{id}", id);

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            _logger.LogInformation("Create get");

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price")] Product product)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Create POST: Invalid");
                return View(product);

            }

            _logger.LogInformation("Added new product");

            await _products.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {


            var product = await _products.GetByIdAsync(id);
            _logger.LogInformation("Edit GET: id:{id}", id);

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price")] Product product)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Create POST: Invalid");
                return View(product);
            }


            await _products.UpdateAsync(product);
            return RedirectToAction(nameof(Index));

        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var product = await _products.GetByIdAsync(id);
            _logger.LogInformation("Delete GET: id{id}", id);


            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            await _products.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
