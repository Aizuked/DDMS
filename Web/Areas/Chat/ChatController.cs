﻿using Core;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Identity;

namespace Web.Areas.Chat;

public class ChatController(DdmsDbContext context, UserService userService) : Controller
{
    private readonly DdmsDbContext _context = context;
    private readonly UserService _userService = userService;
}