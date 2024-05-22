using Core;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Identity;

namespace Web.Areas.Facet;

public class FacetController(DdmsDbContext context, UserService userService) : Controller
{
    
}