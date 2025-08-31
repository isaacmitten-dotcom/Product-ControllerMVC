using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Product_Controller.Data;
using Product_Controller.Models;
using Microsoft.Extensions.Logging;

namespace Product_Controller.Controllers
{
    public class ProductsController : Controller
    {
        private readonly Product_ControllerContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(Product_ControllerContext context, ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Product == null)
            {
                _logger.LogError(new NullReferenceException(), "Product context is null");
                return Problem("Entity set 'MvcMovieContext.Products' is null.");
            }

            var prod = from m in _context.Product
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                _logger.LogWarning("Search string provided: {SearchString}", searchString);
                prod = prod.Where(s => s.Name!.ToUpper().Contains(searchString.ToUpper()));
            }

            var ProductSearchVM = new ProductSearchViewModel
            {
                Products = await prod.ToListAsync()
            };

            return View(ProductSearchVM);
        }






                // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("GET details: id was null");
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                _logger.LogWarning("GET details: product not found");

                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Added new product");
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("GET edit: product id was null");
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                _logger.LogWarning("GET edit: product was not found");
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price")] Product product)
        {
            if (id != product.Id)
            {
                _logger.LogError("POST edit: Mismatched IDs");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("POST edit: Product was updated");
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        _logger.LogWarning("POST edit: Product not found");
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError("POST edit: Concurrency error");
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("GET delete: Delete requested with null ID");
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                _logger.LogWarning("GET delete: Delete requested but product not found for ID {Id}", id);

                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _logger.LogDebug("Deleting product with ID {Id}", id);
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
