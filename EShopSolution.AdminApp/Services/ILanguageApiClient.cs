using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Languages;

namespace EShopSolution.AdminApp.Services
{
    public interface ILanguageApiClient
    {
        Task<ApiResult<List<LanguageViewModel>>> GetAll();

    }
}
