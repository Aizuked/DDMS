using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core;
using Core.Dto.Projects;
using Core.Exceptions;
using Core.Models.Projects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Web.Services.Identity;
using Web.Viewmodels.Projects;

namespace Web.Areas.Project;

public class CommentController(DdmsDbContext context, UserService userService, IMapper mapper, IToastifyService toastify) : Controller
{
    public async Task<IActionResult> Edit(int? id)
    {
        CommentEditDto comment = new();

        if (id.HasValue)
            comment =
                await
                    context
                        .Comments
                        .Where(i => i.Id == id)
                        .ProjectTo<CommentEditDto>(mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync()
                ?? throw new NotifiableException("Не удалось найти указанный комментарий.");

        if (!await userService.OwnsOrInRole(User, comment.AuthorId, ROLES_ADMIN) && comment.Id != default)
            throw new NoRightsException();

        var viewModel = new CommentEditViewModel
        {
            CommentEditDto = comment
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<RedirectToActionResult> Edit(CommentEditViewModel vm)
    {
        var dto = vm.CommentEditDto;

        Comment comment = new()
        {
            AuthorId = (await userService.GetCurrentOrThrow(User)).Id
        };
        if (dto.Id != default)
            comment =
                await
                    context
                        .Comments
                        .Where(i => i.Id == dto.Id)
                        .FirstOrDefaultAsync()
                ?? throw new NotifiableException("Не удалось найти указанный комментарий.");

        var projectId =
            await
                context
                    .ProjectTasks
                    .Where(i => i.Comments.Contains(comment))
                    .FirstOrDefaultAsync();

        if (!await userService.OwnsOrInRole(User, comment.AuthorId, ROLES_ADMIN))
            throw new NoRightsException();

        mapper.Map(dto, comment);

        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);

        return RedirectToAction(nameof(Details), "ProjectTask", new { area = "Project", id = projectId });
    }

    [HttpPost]
    public async Task<RedirectToActionResult> Delete(int id)
    {
        var comment =
            await
                context
                    .Comments
                    .Where(i => i.Id == id)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанный комментарий.");

        var projectId =
            await
                context
                    .ProjectTasks
                    .Where(i => i.Comments.Contains(comment))
                    .FirstOrDefaultAsync();

        if (!await userService.OwnsOrInRole(User, comment.AuthorId, ROLES_ADMIN))
            throw new NoRightsException();

        comment.IsDeleted = true;

        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);

        return RedirectToAction(nameof(Details), "ProjectTask", new { area = "Project", id = projectId });
    }
}