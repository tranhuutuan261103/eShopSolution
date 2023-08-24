using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace eShopSolution.ApiIntegration.Services
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        public ProductApiClient(IHttpContextAccessor httpContextAccessor, 
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration) : base(httpContextAccessor, httpClientFactory,  configuration)
        {
        }

        public async Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request)
        {
            return await PutAsync<bool, CategoryAssignRequest>($"/api/products/{id}/categories", request);
        }

        public async Task<bool> Create(ProductCreateRequest request)
        {
            var requestContent = new MultipartFormDataContent();

            if (request.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "thumbnailImage", request.ThumbnailImage.FileName);
            }

            requestContent.Add(new StringContent(request.Price.ToString()), "price");
            requestContent.Add(new StringContent(request.OriginalPrice.ToString()), "originalPrice");
            requestContent.Add(new StringContent(request.Stock.ToString()), "stock");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name.ToString()), "name");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "description");

            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Details) ? "" : request.Details.ToString()), "details");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoDescription) ? "" : request.SeoDescription.ToString()), "seoDescription");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoTitle) ? "" : request.SeoTitle.ToString()), "seoTitle");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoAlias) ? "" : request.SeoAlias.ToString()), "seoAlias");
            requestContent.Add(new StringContent(request.IsFeatured.ToString()), "isFeatured");
            requestContent.Add(new StringContent(request.LanguageId), "languageId");

            var response = await PostFromFormAsync($"/api/products/", requestContent);
            return response;
        }

        public async Task<ProductViewModel> GetById(int id, string languageId)
        {
            var data = await GetAsyncWithoutApiResult<ProductViewModel>($"/api/products/{id}/{languageId}");
            return data;
        }

		public async Task<List<ProductViewModel>> GetFeaturedProducts(string languageId, int take)
		{
			return await GetListAsyncWithoutApiResult<ProductViewModel>($"/api/products/featured/{languageId}/{take}");
		}

		public async Task<List<ProductViewModel>> GetLatestProducts(string languageId, int take)
		{
			return await GetListAsyncWithoutApiResult<ProductViewModel>($"/api/products/latest/{languageId}/{take}");
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
