﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopSolution.Application.Catalog.Products.Dtos;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductEditRequest request);
        Task<int> Delete(int productId);
        Task<List<ProductViewModel>> GetAll();
        Task<ProductViewModel> GetById(int productId);
    }
}
