﻿using eShopSolution.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Categories
{
    public interface ICategoryService
    {
        public Task<List<CategoryViewModel>> GetAll(string languageId);
        public Task<CategoryViewModel> GetById(int id, string languageId);
    }
}
