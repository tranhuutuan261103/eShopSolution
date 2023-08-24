using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Role;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace eShopSolution.ApiIntegration.Services
{
    public class UserApiClient : BaseApiClient, IUserApiClient
    {
        public UserApiClient(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration) 
            : base(httpContextAccessor, httpClientFactory, configuration)
        {

        }
        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            return await PostAsync<string, LoginRequest>($"/api/users/authenticate", request);
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            return await DeleteAsync<bool>($"/api/users/{id}");
        }

        public async Task<ApiResult<UserViewModel>> GetById(Guid id)
        {
            return await GetAsync<UserViewModel>($"/api/users/{id}");
        }

        public async Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request)
        {
            return await GetAsync<PagedResult<UserViewModel>>($"/api/users/paging?" +
                $"pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyWord={request.KeyWord}");
        }

        public async Task<ApiResult<bool>> RegisterUser(RegisterRequest request)
        {
            return await PostAsync<bool, RegisterRequest>($"/api/users/register", request);
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            return await PutAsync<bool, RoleAssignRequest>($"/api/users/{id}/roles", request);
        }

        public async Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request)
        {
            return await PutAsync<bool, UserUpdateRequest>($"/api/users/{id}", request);
        }
    }
}
