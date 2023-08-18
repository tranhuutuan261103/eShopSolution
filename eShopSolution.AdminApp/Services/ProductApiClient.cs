using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace eShopSolution.AdminApp.Services
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        public ProductApiClient(IHttpContextAccessor httpContextAccessor, 
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration) : base(httpContextAccessor, httpClientFactory,  configuration)
        {
        }

        public async Task<ApiResult<PagedResult<ProductViewModel>>> GetProductsPaging([FromQuery] GetManageProductPagingRequest request)
        {
            return await GetAsync<PagedResult<ProductViewModel>>($"/api/products/paging?"
                + $"pageIndex={request.PageIndex}"
                + $"&pageSize={request.PageSize}"
                + $"&keyWord={request.Keyword}"
                + $"&languageId={request.LanguageId}"
                + $"&categoryId={request.CategoryId}");
        }
    }
}
