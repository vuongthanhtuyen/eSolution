using EShopSolution.AdminApp.Services;
using eShopSolution.ViewModels.System.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text;
using eShopSolution.ViewModels.Common;
using eShopSolution.AdminApp.BaseControllers;

namespace EShopSolution.AdminApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;

        public UserController(IUserApiClient userApiClient, IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var session = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(session))
            {
                return RedirectToAction("Login", "User");
            }

            var request = new GetUserPagingRequest()
            {
                BearerToken = session,
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };


            var data = await _userApiClient.GetUsersPagings(request);

            // Kiểm tra nếu data là null hoặc không có dữ liệu
            if (data == null || data.Items == null || !data.Items.Any())
            {
                // Truyền một đối tượng PageResult rỗng để tránh lỗi null trong view
                data = new PageResult<UserViewModel>
                {
                    Items = new List<UserViewModel>(),
                    TotalRecord = 0
                };
            }

            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create (RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _userApiClient.RegisterUser(request);
            if (result)
                return RedirectToAction("Index");
            return View(result);
        }



        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login", "User");
        }


    }
}
