using System.Security.Claims;
using Core.Exceptions;
using Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.Services.Identity;

public class UserService
{
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly UserManager<User> _userManager;

    public UserService(
        UserManager<User> userManager,
        RoleManager<IdentityRole<int>> roleManager
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<User> GetCurrentOrThrow(ClaimsPrincipal user) =>
        await
            _userManager
                .GetUserAsync(user)
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
}