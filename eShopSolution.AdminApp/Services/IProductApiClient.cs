using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;

namespace eShopSolution.AdminApp.Services
{
    public interface IProductApiClient
    {
        Task<ApiResult<PagedResult<ProductViewModel>>> GetProductsPaging(GetManageProductPagingRequest request);
        Task<bool> Create(ProductCreateRequest request);
    }
}
