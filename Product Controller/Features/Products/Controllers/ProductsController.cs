using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product_Controller.Features.Products.Models;

using Product_Controller.Features.Products.Services;

namespace Product_Controller.Features.Products.Controllers
{
    //  /products
    [Route("products")]

    public class ProductsController : Controller
    {
        private readonly IProductService _products;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService products, ILogger<ProductsController> logger)
        {
            _products = products;
            _logger = logger;
        }


        [HttpGet("")]
        // GET: products
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

        [HttpGet("details/{id}", Name = "ProductDetails")]
        // GET: products/details/5
        public async Task<IActionResult> Details(int id)
        {

            var product = await _products.GetByIdAsync(id);

            _logger.LogInformation("GET details for Id:{id}", id);

            return View(product);
        }


        [HttpGet("create")]
        // GET: products/create
        public IActionResult Create()
        {
            _logger.LogInformation("Create get");

            return View();
        }

        [HttpGet("edit/{id:int}")]
        // GET: products/edit/5
        public async Task<IActionResult> Edit(int id)
        {


            var product = await _products.GetByIdAsync(id);
            _logger.LogInformation("Edit GET: id:{id}", id);

            return View(product);
        }

        [HttpGet("delete/{id:int}")]
        // GET: products/delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var product = await _products.GetByIdAsync(id);
            _logger.LogInformation("Delete GET: id{id}", id);


            return View(product);
        }


        // POST: products/create
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


        // POST: products/edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit/{id:int}")]
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



        // POST: products/delete/5
        [HttpPost("delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _products.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //products/byprice/1.00
        [HttpGet("byprice/{price:decimal}")]

        public async Task<IActionResult> ByPrice(decimal price)
        {
            var all = await _products.GetAllAsync();

            var products = all.Where(product  => product.Price == price);

            var viewModel = new ProductSearchViewModel
            {
                Products = products.ToList(),
            };

                return View("index", viewModel);
        }
    }

}
