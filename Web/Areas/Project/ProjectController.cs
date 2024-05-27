﻿using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core;
using Core.Dto.Projects;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Miscellaneous.Extensions;
using Web.Services.Identity;
using Web.Viewmodels;
using Web.Viewmodels.Projects;

namespace Web.Areas.Project;

public class ProjectController(DdmsDbContext context, UserService userService, IMapper mapper, IToastifyService toastify) : Controller
{
    public async Task<IActionResult> List(ListPaginationFilter filter)
    {
        var projectQuery =
            context
                .Projects
                .Where(i => !i.IsDeleted)
                .AsQueryable();

        if (!User.IsInRole(ROLES_ADMIN))
        {
            var userId = (await userService.GetCurrentOrThrow(User)).Id;

            projectQuery =
                projectQuery
                    .Where(i => i.StudentId == userId ||
                                i.TeacherId == userId ||
                                i.IsPublic);
        }

        var projectListDtos =
            await
                projectQuery
                    .ProjectTo<ProjectListDto>(mapper.ConfigurationProvider)
                    .Paginate(filter)
                    .ToListAsync();

        var viewModel = new ProjectListViewModel
        {
            PageCount = await projectQuery.CountAsync(),
            CurrentPage = filter.CurrentPage,
            PageSize = filter.PageSize,
            ProjectListDtos = projectListDtos
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var project =
            await
                context
                    .Projects
                    .Where(i => i.Id == id)
                    .ProjectTo<ProjectDetailsDto>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанный проект.");

        var userId = (await userService.GetCurrentOrThrow(User)).Id;

        var viewModel = new ProjectDetailsViewModel
        {
            CanEdit =
                User.IsInRole(ROLES_ADMIN) ||
                project.Teacher.Id == userId ||
                project.Student.Id == userId,
            ProjectDetailsDto = project
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var project =
            await
                context
                    .Projects
                    .Where(i => i.Id == id)
                    .ProjectTo<ProjectEditDto>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанный проект.");

        var userId = (await userService.GetCurrentOrThrow(User)).Id;

        if (
            project.StudentId != userId ||
            project.TeacherId != userId ||
            !User.IsInRole(ROLES_ADMIN)
        )
            throw new NoRightsException();

        var viewModel = new ProjectEditViewModel
        {
            ProjectEditDto = project
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task Edit(ProjectEditDto dto)
    {
        var project =
            await
                context
                    .Projects
                    .Where(i => i.Id == dto.Id)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанный проект.");

        var userId = (await userService.GetCurrentOrThrow(User)).Id;

        if (
            project.StudentId != userId ||
            project.TeacherId != userId ||
            !User.IsInRole(ROLES_ADMIN)
        )
            throw new NoRightsException();

        mapper.Map(dto, project);
        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);
    }
}
