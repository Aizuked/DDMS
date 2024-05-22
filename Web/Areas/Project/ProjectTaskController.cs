using Core;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Identity;

namespace Web.Areas.Project;

public class ProjectTaskController(DdmsDbContext context, UserService userService) : Controller
{
    
}