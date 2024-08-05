using eShopSolution.Utilities.Constants;
using EShopSolution.AdminApp.Models;
using EShopSolution.AdminApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EShopSolution.AdminApp.Controllers.Components
{
    public class NavigationViewComponent: ViewComponent
    {
        private readonly ILanguageApiClient _languageApiClient;
        public NavigationViewComponent(ILanguageApiClient languageApiClient) 
        { 
            _languageApiClient = languageApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var languages = await _languageApiClient.GetAll();
            var navigationViewModel = new NavigationViewModel(){
                CurrentLanguageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId),
                Languages = languages.ResultObj
            };
            return View("Default", navigationViewModel);
        }
    }
}
