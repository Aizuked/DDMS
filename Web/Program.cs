using AspNetCoreHero.ToastNotification;
using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using AutoMapper.EquivalencyExpression;
using Core;
using Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Miscellaneous;
using Web.Services.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DdmsDbContext>(
    options =>
    {
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection")
        );
    }
);

builder.Services.AddDbContext<IdentityContext>(
    options =>
    {
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection")
        );
    }
);

builder.Services.AddIdentity<User, IdentityRole<int>>(
    options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequireNonAlphanumeric = false;
        options.SignIn.RequireConfirmedEmail = true;
    }
).AddEntityFrameworkStores<IdentityContext>();

builder.Services.AddToastify(
    options =>
    {
        options.DurationInSeconds = 5;
        options.Position = Position.Right;
        options.Gravity = Gravity.Bottom;
    }
);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddAutoMapper(
    options =>
    {
        options.AddCollectionMappers();
        options.UseEntityFrameworkCoreModel<DdmsDbContext>(builder.Services);
        options.UseEntityFrameworkCoreModel<IdentityContext>(builder.Services);
        options.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
    }, AppDomain.CurrentDomain.GetAssemblies()
);

builder.Services.AddTransient<UserService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseExceptionHandler();

app.UseStaticFiles();

app.UseRouting();

app.EnsureMigrationOfContext<DdmsDbContext>();

app.AssertAutoMapperMappings();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Base}/{action=TestString}/{id?}"
);

app.Run();