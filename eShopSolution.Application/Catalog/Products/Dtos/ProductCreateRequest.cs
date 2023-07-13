using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products.Dtos
{
    public class ProductCreateRequest
    {
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public int Stock { get; set; }
        public int ViewCount { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
