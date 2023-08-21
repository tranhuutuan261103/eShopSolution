using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;

namespace eShopSolution.AdminApp.Services
{
    public interface IProductApiClient
    {
        Task<ApiResult<PagedResult<ProductViewModel>>> GetProductsPaging(GetManageProductPagingRequest request);
        Task<bool> Create(ProductCreateRequest request);
        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);
        Task<ProductViewModel> GetById(int id, string languageId);
    }
}
