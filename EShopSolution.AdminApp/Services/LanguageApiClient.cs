using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Languages;
using eShopSolution.ViewModels.System.Roles;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace EShopSolution.AdminApp.Services
{
    public class LanguageApiClient : BaseApiClient, ILanguageApiClient
    {

        public LanguageApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
            :base(httpClientFactory, configuration, httpContextAccessor)
        {   
        }


        public async Task<ApiResult<List<LanguageViewModel>>> GetAll()
        {
            return await GetAsync<ApiResult<List<LanguageViewModel>>>("/api/languages");
        }
    }
}
