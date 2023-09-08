using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Sales;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Sales
{
	public class SalesService : ISalesService
	{
		private readonly EShopDbContext _context;
		private readonly SignInManager<AppUser> _signInManager;
		public SalesService(EShopDbContext context, SignInManager<AppUser> signInManager)
		{
			_context = context;
			_signInManager = signInManager;
		}
		public async Task<ApiResult<bool>> CreateOrder(CheckoutRequest request)
		{
			var user = await _signInManager.UserManager.FindByNameAsync(request.UserName);
			if (user == null)
			{
				return new ApiErrorResult<bool>("Tài khoản không tồn tại");
			}

			var order = new Order()
			{
				OrderDate = DateTime.Now,
				ShipAddress = request.Address,
				ShipEmail = request.Email,
				ShipName = request.Name,
				ShipPhoneNumber = request.PhoneNumber,
				UserId = user.Id,
				Status = Data.Enums.OrderStatus.InProgress,
			};

			await _context.Orders.AddAsync(order);
			;
			if (_context.SaveChanges() == 0)
			{
				return new ApiErrorResult<bool>("Tạo đơn hàng thất bại");
			}

			if (CanCreateOrder(request.OrderDetails) == true)
			{
				foreach (var item in request.OrderDetails)
				{
					var orderDetail = new OrderDetail()
					{
						OrderId = order.Id,
						ProductId = item.ProductId,
						Price = item.Price,
						Quantity = item.Quantity,
					};

					var product = await _context.Products.FindAsync(item.ProductId);
					if (product == null)
					{
						return new ApiErrorResult<bool>("Sản phẩm không tồn tại");
					}
					product.Stock -= item.Quantity;
					await _context.OrderDetails.AddAsync(orderDetail);
				}

				if (_context.SaveChanges() == 0)
				{
					return new ApiErrorResult<bool>("Tạo đơn hàng thất bại");
				}
			}
			else
			{
				return new ApiErrorResult<bool>("Hàng tồn kho không đủ");
			}

			return new ApiSuccessResult<bool>(true);
		}

		private bool CanCreateOrder(List<OrderDetailViewModel> orderDetailViewModels)
		{
			foreach (var item in orderDetailViewModels)
			{
				var product = _context.Products.Find(item.ProductId);
				if (product == null)
				{
					return false;
				}
				if (product.Stock < item.Quantity)
				{
					return false;
				}
			}
			return true;
		}
	}
}
