using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Core;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Identity;

namespace Web.Areas;

public class BaseController (DdmsDbContext context, UserService userService, IMapper mapper, IToastifyService toastify) : Controller
{
    public ViewResult Index()
    {
        return View("Index");
    }
}