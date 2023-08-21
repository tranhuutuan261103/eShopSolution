using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Role;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace eShopSolution.AdminApp.Services
{
    public class RoleApiClient : BaseApiClient, IRoleApiClient
    {
        public RoleApiClient(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration) 
            : base(httpContextAccessor, httpClientFactory, configuration)
        {

        }

        [HttpGet]
        public async Task<ApiResult<List<RoleViewModel>>> GetAll()
        {
            return await GetListAsync<RoleViewModel>($"/api/roles");
        }
    }
}
