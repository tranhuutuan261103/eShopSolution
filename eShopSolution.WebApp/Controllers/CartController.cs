using eShopSolution.ApiIntegration;
using eShopSolution.ApiIntegration.Services;
using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Sales;
using eShopSolution.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eShopSolution.WebApp.Controllers
{
	public class CartController : Controller
	{
		private readonly IProductApiClient _productApiClient;
		private readonly IOrderApiClient _orderApiClient;
		public CartController(IProductApiClient productApiClient, IOrderApiClient orderApiClient)
		{
			_productApiClient = productApiClient;
			_orderApiClient = orderApiClient;
		}
		public IActionResult Index()
		{			
			return View();
		}

		[HttpGet]
		public IActionResult Checkout()
		{
			return View(GetCheckoutViewModel());
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Checkout(CheckoutViewModel request)
		{
			CheckoutViewModel currentCart = GetCheckoutViewModel();
			List<OrderDetailViewModel> orderDetails = new List<OrderDetailViewModel>();
			foreach (var item in currentCart.CartItems)
			{
				orderDetails.Add(new OrderDetailViewModel()
				{
					ProductId = item.ProductId,
					Quantity = item.Quantity,
					Price = item.Price
				});
			}
			CheckoutRequest checkoutRequest = new CheckoutRequest()
			{
				UserName = User.Identity.Name,
				Address = request.CheckoutRequest.Address,
				Email = request.CheckoutRequest.Email,
				Name = request.CheckoutRequest.Name,
				OrderDetails = orderDetails,
				PhoneNumber = request.CheckoutRequest.PhoneNumber,
			};

			var result = await _orderApiClient.CreateOrder(checkoutRequest);

			if (result.ResultObj == true)
			{
				ViewData["SuccessMsg"] = "Đặt hàng thành công";
				HttpContext.Session.Remove(SystemConstants.Cart);
				return View(currentCart);
			}

			return View(currentCart);
		}

		[HttpGet]
		public IActionResult GetListItems()
		{
			var session = HttpContext.Session.GetString(SystemConstants.Cart);
			if (session != null)
			{
				var list = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
				return Ok(list);
			}
			return Ok(new List<CartItemViewModel>());
		}

		[HttpPost]
		public async Task<IActionResult> AddToCart(int id,string languageId)
		{
			var product = await _productApiClient.GetById(id, languageId);

			List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
			var session = HttpContext.Session.GetString(SystemConstants.Cart);
			if (session != null)
			{
				currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
			}

			if (currentCart.Count() != 0)
			{
				if (currentCart.Any(x => x.ProductId == id))
				{
					foreach (var item in currentCart)
					{
						if (item.ProductId == id)
						{
							break;
						}
					}
				}
				else
				{
					var cartItem = new CartItemViewModel()
					{
						ProductId = id,
						Description = product.Description,
						Image = product.ThumbnailImage,
						Name = product.Name,
						Quantity = 1,
						Price = product.Price
					};
					currentCart.Add(cartItem);
				}
				HttpContext.Session.SetString(SystemConstants.Cart, JsonConvert.SerializeObject(currentCart));
			}
			else
			{
				var cartItem = new CartItemViewModel()
				{
					ProductId = id,
					Description = product.Description,
					Image = product.ThumbnailImage,
					Name = product.Name,
					Quantity = 1,
					Price = product.Price
				};
				currentCart.Add(cartItem);
				HttpContext.Session.SetString(SystemConstants.Cart, JsonConvert.SerializeObject(currentCart));
			}

			return Ok(currentCart);
		}

		public IActionResult UpdateCart(int id, int quantity)
		{
			var session = HttpContext.Session.GetString(SystemConstants.Cart);
			List<CartItemViewModel>? list = new List<CartItemViewModel>();
			if (session != null)
			{
				list = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
				if (list != null)
				{
					foreach (var item in list)
					{
						if (item.ProductId == id)
						{
							item.Quantity = quantity;
							if (item.Quantity == 0)
							{
								list.Remove(item);
							}
							break;
						}
					}
				}
				
				HttpContext.Session.SetString(SystemConstants.Cart, JsonConvert.SerializeObject(list));
			}
			return Ok(list);
		}

		private CheckoutViewModel GetCheckoutViewModel()
		{
			var session = HttpContext.Session.GetString(SystemConstants.Cart);
			List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
			if (session != null)
			{
				currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
			}
			if (currentCart == null)
			{
				currentCart = new List<CartItemViewModel>();
			}
			var checkoutVm = new CheckoutViewModel()
			{
				CartItems = currentCart,
				CheckoutRequest = new CheckoutRequest()
				{

				}
			};
			return checkoutVm;
		}
	}
}
