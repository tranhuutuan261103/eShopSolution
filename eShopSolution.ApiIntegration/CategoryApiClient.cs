using eShopSolution.ViewModels.Catalog.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace eShopSolution.ApiIntegration.Services
{
    public class CategoryApiClient : BaseApiClient, ICategoryApiClient
    {
        public CategoryApiClient(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration) 
            : base(httpContextAccessor, httpClientFactory, configuration)
        {
        }
        public async Task<List<CategoryViewModel>> GetAll(string languageId)
        {
            return await GetListAsyncWithoutApiResult<CategoryViewModel>("/api/categories?languageId=" + languageId);
        }
    }
}
