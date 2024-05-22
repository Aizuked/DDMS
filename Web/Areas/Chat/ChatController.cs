using AutoMapper;
using Core;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Identity;

namespace Web.Areas.Chat;

public class ChatController(DdmsDbContext context, UserService userService, IMapper mapper) : Controller
{
    
}