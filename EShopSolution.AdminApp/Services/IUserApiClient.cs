using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.User;

namespace EShopSolution.AdminApp.Services
{
    public interface IUserApiClient
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);
        Task<ApiResult<bool>> RegisterUser(RegisterRequest registerRequest);

        Task<ApiResult<PageResult<UserViewModel>>> GetUsersPagings(GetUserPagingRequest request);

        Task<ApiResult<bool>> UpdateUser (Guid id, UserUpdateRequest request);
        Task<ApiResult<UserViewModel>> GetById (Guid id);
       

    }
}
