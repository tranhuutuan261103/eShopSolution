using eShopSolution.ApiIntegration.Services;
using eShopSolution.ViewModels.Utilities.Slides;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
	public class SlideApiClient : BaseApiClient, ISlideApiClient
	{
		public SlideApiClient(IHttpContextAccessor httpContextAccessor , IHttpClientFactory httpClientFactory, IConfiguration configuration) 
			: base(httpContextAccessor, httpClientFactory, configuration)
		{
		}

		public async Task<List<SlideViewModel>> GetAll()
		{
			return await GetListAsyncWithoutApiResult<SlideViewModel>("/api/slides");
		}
	}
}
