using AspNetCoreHero.ToastNotification.Abstractions;
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

        return View("ProjectList", viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var project =
            await
                context
                    .Projects
                    .Where(i => i.Id == id)
                    .Include(i => i.Student)
                    .Include(i => i.Teacher)
                    .Include(i => i.Status)
                    .Include(i => i.Theme)
                    .ProjectTo<ProjectDetailsDto>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанный проект.");

        var userId = (await userService.GetCurrentOrThrow(User)).Id;

        var viewModel = new ProjectDetailsViewModel
        {
            CanEdit =
                User.IsInRole(ROLES_ADMIN) ||
                project.Teacher?.Id == userId ||
                project.Student?.Id == userId,
            ProjectDetailsDto = project
        };

        return View("ProjectDetails", viewModel);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        ProjectEditDto project = new();

        if (id.HasValue)
            project =
                await
                    context
                        .Projects
                        .Where(i => i.Id == id)
                        .ProjectTo<ProjectEditDto>(mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync()
                ?? throw new NotifiableException("Не удалось найти указанный проект.");

        var userId = (await userService.GetCurrentOrThrow(User)).Id;

        if (
            project.StudentId != userId &&
            project.TeacherId != userId &&
            !User.IsInRole(ROLES_ADMIN) &&
            project.Id != default
        )
            throw new NoRightsException();

        var viewModel = new ProjectEditViewModel
        {
            ProjectEditDto = project
        };

        return View("ProjectEdit", viewModel);
    }

    [HttpPost]
    public async Task<RedirectToActionResult> Edit(ProjectEditViewModel vm)
    {
        var dto = vm.ProjectEditDto;
        var userId = (await userService.GetCurrentOrThrow(User)).Id;

        Core.Models.Projects.Project project = new()
        {
            StudentId = userId,
            StatusId = (await context.FacetItems.Where(i => i.Code == "Discussion").FirstAsync()).Id,
            TeacherId = vm.ProjectEditDto.TeacherId
        };

        if (dto.Id != default)
            project =
                await
                    context
                        .Projects
                        .Where(i => i.Id == dto.Id)
                        .FirstOrDefaultAsync()
                ?? throw new NotifiableException("Не удалось найти указанный проект.");

        if (
            project.StudentId != userId &&
            project.TeacherId != userId &&
            !User.IsInRole(ROLES_ADMIN)
        )
            throw new NoRightsException();

        mapper.Map(dto, project);
        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);

        return RedirectToAction(nameof(List), new { });
    }

    [HttpPost]
    public async Task<RedirectToActionResult> SelectTheme(int projectId, int suggestedThemeId)
    {
        var project =
            await
                context
                    .Projects
                    .Include(project => project.Theme)
                    .Where(i => i.Id == projectId)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанный проект.");

        if (!await userService.OwnsOrInRole(User, project.StudentId, ROLES_TEACHER))
            throw new NoRightsException();

        var suggestedTheme =
            await
                context
                    .SuggestedThemes
                    .Where(i => i.Id == suggestedThemeId)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанную тему.");

        if (!await userService.OwnsOrInRole(User, suggestedTheme.UserId, ROLES_TEACHER))
            throw new NoRightsException();

        var theme =
            project.Theme
            ?? new Core.Models.Themes.Theme
            {
                SelectedThemeId = suggestedTheme.Id
            };

        if (theme.Id == default)
        {
            context.Add(theme);
            await context.SaveChangesAsync();

            project.ThemeId = theme.Id;
            await context.SaveChangesAsync();
        }
        else
        {
            theme.ApproverId = null;
            theme.Approver = null;
            theme.IsApproved = false;
            theme.SelectedThemeToChangeId = suggestedTheme.Id;

            await context.SaveChangesAsync();
        }

        toastify.Success(NOTIFY_SUCCESS);

        return RedirectToAction(nameof(List), new { });
    }

    [HttpPost]
    public async Task<RedirectToActionResult> ApproveTheme(int id)
    {
        var theme =
            await
                context
                    .Themes
                    .Where(i => i.Id == id)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанную тему.");

        if (theme.IsApproved)
            return RedirectToAction(nameof(List), new { });

        var project =
            await
                context
                    .Projects
                    .Where(i => i.ThemeId == id)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти проект с такой темой.");

        if (!await userService.OwnsOrInRole(User, project.TeacherId, ROLES_TEACHER))
            throw new NoRightsException();

        theme.ApproverId = (await userService.GetCurrentOrThrow(User)).Id;
        theme.IsApproved = true;

        if (theme.SelectedThemeToChangeId.HasValue)
        {
            theme.SelectedThemeId = theme.SelectedThemeToChangeId;
            theme.SelectedThemeToChangeId = null;
            theme.SelectedThemeToChange = null;
        }

        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);

        return RedirectToAction(nameof(List), new { });
    }
}
