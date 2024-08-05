using eShopSolution.Data.EF;
using Microsoft.EntityFrameworkCore;
using eShopSolution.Utilities.Constants;
using eShopSolution.Application.Common;
using Microsoft.AspNetCore.Identity;
using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.System.User;
using eShopSolution.Application.System.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using eShopSolution.Application.Catolog.Products;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using FluentValidation;
using eShopSolution.Application.System.Roles;
using eShopSolution.Application.System.Languages;

var builder = WebApplication.CreateBuilder(args);
//ConfigureService in Startup
IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
string issuer = configuration.GetValue<string>("Tokens:Issuer");
string signingKey = configuration.GetValue<string>("Tokens:Key");


// Đăng ký dịch vụ
//Declare DI

builder.Services.AddHttpClient();
builder.Services.AddTransient<IStorageService, FileStorageService>();
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<EShopDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// có nghĩa là: nếu chúng ta yêu cầu IPublixProducServide thì nó sẽ trả về PublicProducService
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<ILanguageService, LanguageService>();
builder.Services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
builder.Services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
builder.Services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
builder.Services.AddRazorPages();

//builder.Services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
//builder.Services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation()
        .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger eShop Solution", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });
});


byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = issuer,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = System.TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
    };
});

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    // Default Lockout settings.
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    options.Lockout.MaxFailedAccessAttempts = 5;
//    options.Lockout.AllowedForNewUsers = true;
//    options.SignIn.RequireConfirmedEmail = false;
//    options.SignIn.RequireConfirmedPhoneNumber = false;

//});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 1;
});



builder.Services.AddAuthorization();

builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});


var connectionString = configuration.GetConnectionString(SystemConstants.MainConnectionString);
builder.Services.AddDbContext<EShopDbContext>(options =>
{
    options.UseSqlServer(connectionString); // Cấu hình kết nối đến cơ sở dữ liệu của bạn
});



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
}


//app.UseHttpsRedirection();\
app.UseSwagger(x => x.SerializeAsV2 = true);



app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();


app.UseSession();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();

