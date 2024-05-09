using AspNetCoreHero.ToastNotification;
using Core;
using Core.Models.Identitiy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Web.Miscellanious;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DdmsDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(DdmsDbContext)));
    }
);

builder.Services.AddDbContext<IdentityContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(IdentityContext)));
    }
);

builder.Services.AddIdentity<User, string>(
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


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.UseExceptionHandler();

app.Run();