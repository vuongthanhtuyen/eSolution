using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Roles;

namespace EShopSolution.AdminApp.Services
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleViewModel>>> GetAll();

    }
}
