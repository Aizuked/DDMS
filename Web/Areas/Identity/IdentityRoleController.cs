using Core;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Identity;

namespace Web.Areas.Identity;

public class IdentityRoleController(DdmsDbContext context, UserService userService) : Controller
{
    
}