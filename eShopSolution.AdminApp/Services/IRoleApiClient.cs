using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Role;

namespace eShopSolution.AdminApp.Services
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleViewModel>>> GetAll();
    }
}
