﻿using eShopSolution.ViewModels.Catalog.Categories;

namespace eShopSolution.ApiIntegration.Services
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryViewModel>> GetAll(string languageId);
        Task<CategoryViewModel> GetById(int id, string languageId);
    }
}
