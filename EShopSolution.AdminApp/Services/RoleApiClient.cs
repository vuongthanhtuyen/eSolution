using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Roles;
using eShopSolution.ViewModels.System.User;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace EShopSolution.AdminApp.Services
{


    //public class RoleApiClient : BaseApiClient, IRoleApiClient
    //{
    //    public RoleApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    //: base(httpClientFactory, configuration, httpContextAccessor)
    //    {
    //    }


    //    public async Task<ApiResult<List<RoleViewModel>>> GetAll()
    //    {
    //        return await GetAsync<ApiResult<List<RoleViewModel>>>("/api/Roles");
    //    }
    //}

    public class RoleApiClient : IRoleApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public RoleApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResult<List<RoleViewModel>>> GetAll()
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.GetAsync($"/api/Roles");
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                List<RoleViewModel> myDeserializedObjList = (List<RoleViewModel>)JsonConvert.DeserializeObject(body, typeof(List<RoleViewModel>));
                return new ApiSeccessResult<List<RoleViewModel>>(myDeserializedObjList);
            }

            return JsonConvert.DeserializeObject<ApiErrorResult<List<RoleViewModel>>>(body);
        }
    }
}
