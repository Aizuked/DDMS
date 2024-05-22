using Core;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Identity;

namespace Web.Areas.Project;

public class ProjectController(DdmsDbContext context, UserService userService) : Controller
{
    
}