using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Sales
{
	public interface ISalesService
	{
		Task<ApiResult<bool>> CreateOrder(CheckoutRequest request);
	}
}
