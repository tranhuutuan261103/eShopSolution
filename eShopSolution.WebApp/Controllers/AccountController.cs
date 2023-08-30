using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eShopSolution.ApiIntegration.Services;
using NuGet.Common;

namespace eShopSolution.WebApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUserApiClient _userApiClient;
		public AccountController(IUserApiClient userApiClient)
		{
			_userApiClient = userApiClient;
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginRequest request)
		{
			if (!ModelState.IsValid) // if login not success
			{
				return View(ModelState);
			}

			var token = await _userApiClient.Authenticate(request);

			if (token.ResultObj == null)
			{
				ModelState.AddModelError("", token.Message);
				return View();
			}

			var userPrincipal = ValidateToken(token.ResultObj);
			var authProperties = new AuthenticationProperties
			{
				ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
				IsPersistent = false
			};

			HttpContext.Session.SetString("Token", token.ResultObj);

			await HttpContext.SignInAsync(
							   CookieAuthenticationDefaults.AuthenticationScheme,
											  userPrincipal,
															 authProperties);

			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterRequest registerRequest)
		{
			if (!ModelState.IsValid)
			{
				return View(registerRequest);
			}

			var result = await _userApiClient.RegisterUser(registerRequest);
			if (result.IsSuccessed == false)
			{
				ModelState.AddModelError("", result.Message);
				return RedirectToAction("Register", "Account", registerRequest);
			}

			var loginResult = await _userApiClient.Authenticate(new LoginRequest()
			{
				UserName = registerRequest.UserName,
				Password = registerRequest.Password,
				RememberMe = true
			});

			var userPrincipal = ValidateToken(loginResult.ResultObj);
			var authProperties = new AuthenticationProperties
			{
				ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
				IsPersistent = false
			};

			HttpContext.Session.SetString("Token", loginResult.ResultObj);

			await HttpContext.SignInAsync(
							   CookieAuthenticationDefaults.AuthenticationScheme,
											  userPrincipal,
															 authProperties);

			return RedirectToAction("Index", "Home");
		}

		private ClaimsPrincipal ValidateToken(string jwtToken)
		{
			IdentityModelEventSource.ShowPII = true;

			SecurityToken validatedToken;
			TokenValidationParameters validationParameters = new TokenValidationParameters();

			validationParameters.ValidateLifetime = true; // check if expired
			validationParameters.ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 };

			validationParameters.ValidAudience = "http://localhost:7040";
			validationParameters.ValidIssuer = "http://localhost:7040";
			validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eShopSolutionSecretKeySymmetricSecurityKey"));

			ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

			return claimsPrincipal;

		}
	}
}
