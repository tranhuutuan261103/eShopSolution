using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Languages;

namespace eShopSolution.AdminApp.Services
{
    public interface ILanguageApiClient
    {
        public Task<ApiResult<List<LanguageViewModel>>> GetAll();
    }
}
