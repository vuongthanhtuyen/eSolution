﻿using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace eShopSolution.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResult<string>> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return new ApiErrorResult<string> ("Tài khoản không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>("Đăng nhập không đúng");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // Thêm ClaimTypes.NameIdentifier để lưu ID người dùng

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return new ApiSeccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }



        public async Task<ApiResult<PageResult<UserViewModel>>> GetUserPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword)
                || x.PhoneNumber.Contains(request.Keyword));
            }

            // Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserViewModel()
                {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Id = x.Id,
                    UserName = x.UserName
                }).ToListAsync();

            // select and projection
            var pageResult = new PageResult<UserViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data

            };
            return new ApiSeccessResult<PageResult<UserViewModel>>(pageResult); 
            
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if(user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }
            if(await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }



            user = new AppUser()
            {
                Dob = request.Dob,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new ApiSeccessResult<bool>();
            }
            return new ApiErrorResult<bool> ("Đăng ký không thành công");
        }

        public async Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request)
        {
            if(await _userManager.Users.AnyAsync(x=>x.Email == request.Email && x.Id != id))
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.Dob = request.Dob;
            user.Email = request.Email;
            user.FirstName = request.FirstName; 
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSeccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công");
        }


        public async Task<ApiResult<UserViewModel>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserViewModel>("User Không tồn tại");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var userVM = new UserViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Dob = user.Dob,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Roles = roles
            };
            return new ApiSeccessResult<UserViewModel>(userVM);
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == id.ToString())
            {
                return new ApiErrorResult<bool>("Không thể xóa tài khoản đang đăng nhập");

            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) 
            {
                return new ApiErrorResult<bool>("User không tồn tại");
            }
            await _userManager.DeleteAsync(user);
            return new ApiSeccessResult<bool>();

        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("User Không tồn tại");
            }

            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles) 
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);

                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);
            
            var addRoles = request.Roles.Where(x=> x.Selected).Select(x=>x.Name).ToList();

            foreach (var roleName in addRoles)
            {
                if(await _userManager.IsInRoleAsync(user,roleName)== false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }
            return new ApiSeccessResult<bool>();
        }
    }
}