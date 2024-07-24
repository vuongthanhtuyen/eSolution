using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.User;

namespace EShopSolution.AdminApp.Services
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);
        Task<PageResult<UserViewModel>> GetUsersPagings(GetUserPagingRequest request);
        Task<bool> RegisterUser(RegisterRequest registerRequest);

    }
}
