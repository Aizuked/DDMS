using Core;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Identity;

namespace Web.Areas.Project;

public class CommentController(DdmsDbContext context, UserService userService) : Controller
{
    
}