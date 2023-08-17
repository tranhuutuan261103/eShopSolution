using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Languages;
using eShopSolution.ViewModels.System.Role;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace eShopSolution.AdminApp.Services
{
    public class LanguageApiClient : BaseApiClient, ILanguageApiClient
    {
        public LanguageApiClient(IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
            : base(httpContextAccessor, httpClientFactory, configuration) { }

        [HttpGet]
        public async Task<ApiResult<List<LanguageViewModel>>> GetAll()
        {
            return await GetListAsync<LanguageViewModel>("/api/languages");
        }
    }
}
