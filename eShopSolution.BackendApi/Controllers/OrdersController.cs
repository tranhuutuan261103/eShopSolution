using eShopSolution.Application.Sales;
using eShopSolution.ViewModels.Sales;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
	[Route("api/Orders")]
	[ApiController]
	public class OrdersController : Controller
	{
		private readonly ISalesService _salesService;
		public OrdersController(ISalesService salesService)
		{
			_salesService = salesService;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CheckoutRequest request)
		{
			var result = await _salesService.CreateOrder(request);
			if (result.IsSuccessed)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}
	}
}
