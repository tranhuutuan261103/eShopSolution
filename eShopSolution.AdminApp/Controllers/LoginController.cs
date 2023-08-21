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

namespace eShopSolution.AdminApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;

        public LoginController(IUserApiClient userApiClient,
            IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest request)
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
            HttpContext.Session.SetString("DefaultLanguage", "vi-VN");

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
