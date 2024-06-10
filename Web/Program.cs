global using static Core.Constants.Constants;
using AspNetCoreHero.ToastNotification;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Core;
using Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Miscellaneous;
using Web.Services.Chat;
using Web.Services.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(opts =>
    {
        opts.AllowEmptyInputInBodyModelBinding = true;
    }
);

builder.Services.AddDbContext<DdmsDbContext>(
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
        options.SignIn.RequireConfirmedEmail = false;
    }
).AddEntityFrameworkStores<DdmsDbContext>();

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
        options.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
    }
);

builder.Services.AddSignalR();
builder.Services.AddRazorPages();

builder.Services.AddTransient<UserService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseExceptionHandler();

app.UseStaticFiles();

app.UseRouting();


app.AssertAutoMapperMappings();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Base}/{action=Index}/{id?}"
);
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Base}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapHub<ChatHub>("/chat");

app.EnsureMigrationOfContext<DdmsDbContext>();

app.Run();