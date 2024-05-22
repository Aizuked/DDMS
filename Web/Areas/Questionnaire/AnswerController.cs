using AutoMapper;
using Core;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Identity;

namespace Web.Areas.Questionnaire;

public class AnswerController(DdmsDbContext context, UserService userService, IMapper mapper) : Controller
{
    
}