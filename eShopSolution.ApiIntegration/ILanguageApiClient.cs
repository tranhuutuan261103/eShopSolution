using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Languages;

namespace eShopSolution.ApiIntegration.Services
{
    public interface ILanguageApiClient
    {
        public Task<ApiResult<List<LanguageViewModel>>> GetAll();
    }
}
