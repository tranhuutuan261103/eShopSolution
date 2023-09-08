using eShopSolution.ApiIntegration.Services;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Sales;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
	public class OrderApiClient : BaseApiClient, IOrderApiClient
	{
		public OrderApiClient(IHttpContextAccessor httpContextAccessor,
							IHttpClientFactory httpClientFactory,
							IConfiguration configuration) : base(httpContextAccessor, httpClientFactory, configuration)
		{
		}
		public async Task<ApiResult<bool>> CreateOrder(CheckoutRequest request)
		{
			var result = await PostAsync<bool, CheckoutRequest>("/api/orders", request);
			return result;
		}
	}
}
