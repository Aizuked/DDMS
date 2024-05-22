using AutoMapper;
using Core;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Identity;

namespace Web.Areas.Theme;

public class SuggestedThemeController(DdmsDbContext context, UserService userService, IMapper mapper) : Controller
{
    
}