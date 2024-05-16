using AspNetCoreHero.ToastNotification;
using Core;
using Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Miscellaneous;

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


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseExceptionHandler();

app.UseStaticFiles();

app.UseRouting();

app.EnsureMigrationOfContext<IdentityContext>();
app.EnsureMigrationOfContext<DdmsDbContext>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Base}/{action=TestString}/{id?}"
);

app.Run();