﻿using Core;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Identity;

namespace Web.Areas.Theme;

public class ThemeController(DdmsDbContext context, UserService userService) : Controller
{
    private readonly DdmsDbContext _context = context;
    private readonly UserService _userService = userService;
}