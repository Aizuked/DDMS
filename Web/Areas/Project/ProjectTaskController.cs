using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core;
using Core.Dto.Projects;
using Core.Exceptions;
using Core.Models.Projects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Miscellaneous.Extensions;
using Web.Services.Identity;
using Web.Viewmodels;
using Web.Viewmodels.Projects;

namespace Web.Areas.Project;

public class ProjectTaskController(DdmsDbContext context, UserService userService, IMapper mapper, IToastifyService toastify) : Controller
{
    public async Task<IActionResult> List(int projectId, ListPaginationFilter filter)
    {
        var projectTaskQuery =
            context
                .ProjectTasks
                .Where(i => i.ProjectId == projectId)
                .AsQueryable();

        var projectTaskListDtos =
            await
                projectTaskQuery
                    .ProjectTo<ProjectTaskListDto>(mapper.ConfigurationProvider)
                    .Paginate(filter)
                    .ToListAsync();

        var viewModel = new ProjectTaskListViewModel
        {
            PageCount = await projectTaskQuery.CountAsync(),
            CurrentPage = filter.CurrentPage,
            PageSize = filter.PageSize,
            ProjectTaskListDtos = projectTaskListDtos
        };

        return View("ProjectTaskList", viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var projectTask =
            await
                context
                    .ProjectTasks
                    .Where(i => i.Id == id)
                    .ProjectTo<ProjectTaskDetailsDto>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанный проект.");

        var userId = (await userService.GetCurrentOrThrow(User)).Id;

        if (
            projectTask.Project.Student.Id != userId &&
            projectTask.Project.Teacher.Id != userId &&
            !User.IsInRole(ROLES_ADMIN)
        )
            projectTask.Comments =
                projectTask
                    .Comments
                    .Where(i => i is { IsDeleted: false, IsPrivate: false })
                    .Select(i => i)
                    .ToList();

        var viewModel = new ProjectTaskDetailsViewModel
        {
            CanEdit =
                User.IsInRole(ROLES_ADMIN) ||
                projectTask.Project.Teacher.Id == userId &&
                projectTask.Project.Student.Id == userId,
            ProjectTaskDetailsDto = projectTask
        };

        return View("ProjectTaskDetails", viewModel);
    }

    public async Task<IActionResult> Edit(int? id, int? projectId)
    {
        ProjectTaskEditDto projectTask = new()
        {
            Project = new ProjectListDto(){Id = (int)projectId}
        };

        if (id.HasValue)
            projectTask =
                await
                    context
                        .ProjectTasks
                        .Where(i => i.Id == id)
                        .ProjectTo<ProjectTaskEditDto>(mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync()
                ?? throw new NotifiableException("Не удалось найти указанную задачу.");

        var userId = (await userService.GetCurrentOrThrow(User)).Id;

        if (
            projectTask.Project?.Student.Id != userId &&
            projectTask.Project?.Teacher.Id != userId &&
            !User.IsInRole(ROLES_ADMIN) &&
            projectTask.Id != default
        )
            throw new NoRightsException();

        var viewModel = new ProjectTaskEditViewModel
        {
            ProjectTaskEditDto = projectTask
        };

        return View("ProjectTaskEdit", viewModel);
    }

    [HttpPost]
    public async Task<RedirectToActionResult> Edit(ProjectTaskEditViewModel vm, int? projectId)
    {
        var dto = vm.ProjectTaskEditDto;
        var userId = (await userService.GetCurrentOrThrow(User)).Id;

        ProjectTask projectTask = new ProjectTask()
        {
            ProjectId = (int)projectId,
            Project = (await context.Projects.Where(i => i.Id == projectId).FirstAsync()),
            AuthorId = userId,
            StatusId = (await context.FacetItems.Where(i => i.Code == "Analysis").FirstAsync()).Id
        };

        if (dto.Id != default)
            projectTask =
                await
                    context
                        .ProjectTasks
                        .Include(i => i.Project)
                        .Where(i => i.Id == dto.Id)
                        .FirstOrDefaultAsync()
                ?? throw new NotifiableException("Не удалось найти указанную задачу.");

        if (
            projectTask.Project.StudentId != userId &&
            projectTask.Project.TeacherId != userId &&
            !User.IsInRole(ROLES_ADMIN)
        )
            throw new NoRightsException();

        mapper.Map(dto, projectTask);

        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);

        return RedirectToAction(nameof(List), new { projectId = projectId });
    }

    [HttpPost]
    public async Task<RedirectToActionResult> Delete(int id)
    {
        var projectTask =
            await
                context
                    .ProjectTasks
                    .Include(projectTask => projectTask.Project)
                    .Where(i => i.Id == id)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанную задачу.");

        var userId = (await userService.GetCurrentOrThrow(User)).Id;

        if (
            projectTask.Project.StudentId != userId &&
            projectTask.Project.TeacherId != userId &&
            !User.IsInRole(ROLES_ADMIN)
        )
            throw new NoRightsException();

        projectTask.IsDeleted = true;

        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);

        return RedirectToAction(nameof(List), new { projectId = projectTask.ProjectId });
    }
}
