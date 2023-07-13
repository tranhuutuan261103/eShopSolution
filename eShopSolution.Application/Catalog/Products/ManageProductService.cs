using eShopSolution.Application.Catalog.Products.Dtos;
using eShopSolution.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopSolution.Data.Entities;

namespace eShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly EShopDbContext _context;
        public ManageProductService(EShopDbContext context) 
        {
            _context = context;
        }
        public Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = request.ViewCount,
                DateCreated = DateTime.Now
            };
            return _context.SaveChangesAsync();
        }

        public Task<int> Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> GetById(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(ProductEditRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
