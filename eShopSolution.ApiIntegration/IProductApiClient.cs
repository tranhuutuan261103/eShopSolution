using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;

namespace eShopSolution.ApiIntegration.Services
{
    public interface IProductApiClient
    {
        Task<ApiResult<PagedResult<ProductViewModel>>> GetProductsPaging(GetManageProductPagingRequest request);
        Task<bool> Create(ProductCreateRequest request);
        Task<bool> Update(ProductUpdateRequest request);
        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);
        Task<ProductViewModel> GetById(int id, string languageId);
        Task<List<ProductViewModel>> GetFeaturedProducts(string languageId, int take); 
        Task<List<ProductViewModel>> GetLatestProducts(string languageId, int take);
    }
}
