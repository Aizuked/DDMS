using System.Security.Claims;
using Core;
using Core.Exceptions;
using Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.Services.Identity;

public class UserService
{
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly UserManager<User> _userManager;
    private readonly DdmsDbContext _context;

    public UserService(
        UserManager<User> userManager,
        RoleManager<IdentityRole<int>> roleManager,
        DdmsDbContext context
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    public async Task<User?> TryGetCurrent(ClaimsPrincipal user) =>
        await
            _userManager
                .GetUserAsync(user);

    public async Task<User> GetCurrentOrThrow(ClaimsPrincipal user) =>
        await TryGetCurrent(user)
        ?? throw new NotifiableException("Не удалось распознать текущего пользователя.");

    public async Task<IEnumerable<string>> GetCurrentRoles(ClaimsPrincipal user)
    {
        var currentUser = await GetCurrentOrThrow(user);

        return
            await
                _userManager
                    .GetRolesAsync(currentUser);
    }

    public IQueryable<User> GetUserQuery() =>
        _userManager
            .Users;

    public IQueryable<User> UserById(int id) =>
        GetUserQuery()
            .Where(i => i.Id == id);

    public async Task<User> UserByIdOrThrow(int id) =>
        await
            UserById(id)
                .FirstOrDefaultAsync()
        ?? throw new NotifiableException("Не удалось найти указанного пользователя.");

    public async Task AddUserRole(int id, string userRole) =>
        await
            _userManager
                .AddToRoleAsync(await UserByIdOrThrow(id), userRole);

    public async Task RemoveUserRole(int id, string userRole) =>
        await
            _userManager
                .RemoveFromRoleAsync(await UserByIdOrThrow(id), userRole);

    public async ValueTask<bool> InRoleAsync(int id, string userRole) =>
        await
            _userManager
                .IsInRoleAsync(await UserByIdOrThrow(id), userRole);

    public async ValueTask<bool> OwnsOrInRole(ClaimsPrincipal user, int userId, string userRole) =>
        user.IsInRole(userRole) ||
        (await GetCurrentOrThrow(user)).Id == userId;

    public async Task TryToggleOnline(ClaimsPrincipal? user, bool? isOnline = null)
    {
        if (user == null)
            return;

        var localUser = await TryGetCurrent(user);

        if (localUser != null)
        {
            localUser.IsOnline = isOnline ?? !localUser.IsOnline;
            localUser.LastOnline =
                localUser.IsOnline == false
                    ? DateTime.Now
                    : null;

            _context.Entry(localUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
