using IHaveAnIdea.Business.Abstract;
using IHaveAnIdea.Business.Concrete;
using IHaveAnIdea.Business.Mapping;
using IHaveAnIdea.DataAccess.Abstract;
using IHaveAnIdea.DataAccess.Concrete.EntityFramework;
using IHaveAnIdea.DataAccess.Context;
using IHaveAnIdea.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
});

// Repositories
builder.Services.AddScoped<IPostRepository, EfPostRepository>();
builder.Services.AddScoped<ICommentRepository, EfCommentRepository>();
builder.Services.AddScoped<ILikeRepository, EfLikeRepository>();
builder.Services.AddScoped<ICategoryRepository, EfCategoryRepository>();

// Services
builder.Services.AddScoped<IPostService, PostManager>();
builder.Services.AddScoped<ICommentService, CommentManager>();
builder.Services.AddScoped<ILikeService, LikeManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
