using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Core;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Identity;

namespace Web.Areas.Identity;

public class IdentityRoleController(DdmsDbContext context, UserService userService, IMapper mapper, IToastifyService toastify) : Controller
{
    [HttpPost]
    public async Task<RedirectToActionResult> GrantRole(int userId, string roleName)
    {
        if (!User.IsInRole(ROLES_ADMIN))
            throw new NoRightsException();

        await userService.AddUserRole(userId, roleName);

        toastify.Success(NOTIFY_SUCCESS);

        return RedirectToAction("List", "User", new { area = "Identity"});
    }

    [HttpPost]
    public async Task<RedirectToActionResult> RemoveRole(int userId, string roleName)
    {
        if (!User.IsInRole(ROLES_ADMIN))
            throw new NoRightsException();

        await userService.RemoveUserRole(userId, roleName);

        toastify.Success(NOTIFY_SUCCESS);

        return RedirectToAction("List", "User", new { area = "Identity"});
    }
}