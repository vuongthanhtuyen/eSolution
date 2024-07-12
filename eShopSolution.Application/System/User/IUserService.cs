using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.System.User
{
    public interface IUserService
    {
        Task<String> Authencate(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
    }
}
