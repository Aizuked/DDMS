using Core;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Identity;

namespace Web.Areas.Files;

public class LocalFileController(DdmsDbContext context, UserService userService) : Controller
{
    
}