using AutoMapper;
using Core;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Identity;

namespace Web.Areas.Theme;

public class KeyWordController(DdmsDbContext context, UserService userService, IMapper mapper) : Controller
{
    
}