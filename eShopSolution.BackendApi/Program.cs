using System;
using eShopSolution.Application.Catolog.Products;
using eShopSolution.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
//using eshopsolution.apiintegration;
//using eshopsolution.viewmodels.system.users;
//using fluentvalidation.aspnetcore;
using Microsoft.AspNetCore.Authentication.Cookies;  
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using eShopSolution.Utilities.Constants;
using eShopSolution.Application.Common;
using Microsoft.AspNetCore.Identity;
using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.System.User;
using eShopSolution.Application.System.Users;
var builder = WebApplication.CreateBuilder(args);
//ConfigureService in Startup
builder.Services.AddControllers();


builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger EShopSolution", Version = "v1" });
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.AccessDeniedPath = "/User/Forbidden/";
    });

//builder.Services.AddControllersWithViews()
//         .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

var connectionString = configuration.GetConnectionString(SystemConstants.MainConnectionString);
builder.Services.AddDbContext<EShopDbContext>(options =>
{
    options.UseSqlServer(connectionString); // Cấu hình kết nối đến cơ sở dữ liệu của bạn
});

//Declare DI
builder.Services.AddTransient<IStorageService, FileStorageService>();
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores< EShopDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// có nghĩa là: nếu chúng ta yêu cầu IPublixProducServide thì nó sẽ trả về PublicProducService
builder.Services.AddTransient<IPublicProductService, PublicProductService>();
builder.Services.AddTransient<IManageProductService, ManageProductService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
builder.Services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
builder.Services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
builder.Services.AddRazorPages();


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}


//app.UseHttpsRedirection();\
app.UseSwagger(x=>x.SerializeAsV2 = true);



app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();