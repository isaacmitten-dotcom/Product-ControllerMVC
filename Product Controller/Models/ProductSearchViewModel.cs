
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Product_Controller.Models
{
    public class ProductSearchViewModel
    {
        public List<Product>? Products { get; set; }
        public string? SearchString { get; set; }
    }
}
