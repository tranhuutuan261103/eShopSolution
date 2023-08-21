using eShopSolution.AdminApp.Models;
using eShopSolution.ApiIntegration.Services;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.AdminApp.Controllers.Components
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly ILanguageApiClient languageApiClient;
        public NavigationViewComponent(ILanguageApiClient languageApiClient)
        {
            this.languageApiClient = languageApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var languages = await languageApiClient.GetAll();
            var currentLanguageId = HttpContext
                .Session
                .GetString("DefaultLanguage");
            NavigationViewModel navigationVm = new NavigationViewModel()
            {
                CurrentLanguageId = currentLanguageId,
                Languages = languages.ResultObj
            };
            return View("Default", navigationVm);
        }
    }
}
