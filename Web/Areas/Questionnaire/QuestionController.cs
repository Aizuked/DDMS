using Core;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Identity;

namespace Web.Areas.Questionnaire;

public class QuestionController(DdmsDbContext context, UserService userService) : Controller
{
    
}