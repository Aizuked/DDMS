using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core;
using Core.Dto.Projects;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Services.Identity;
using Web.Viewmodels.Projects;

namespace Web.Areas.Project;

public class CommentController(DdmsDbContext context, UserService userService, IMapper mapper, IToastifyService toastify) : Controller
{
    public async Task<IActionResult> Edit(int id)
    {
        var comment =
            await
                context
                    .Comments
                    .Where(i => i.Id == id)
                    .ProjectTo<CommentEditDto>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанный комментарий.");

        if (!await userService.OwnsOrInRole(User, comment.AuthorId, ROLES_ADMIN))
            throw new NoRightsException();

        var viewModel = new CommentEditViewModel
        {
            CommentEditDto = comment
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task Edit(CommentEditDto dto)
    {
        var comment =
            await
                context
                    .Comments
                    .Where(i => i.Id == dto.Id)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанный комментарий.");

        if (!await userService.OwnsOrInRole(User, comment.AuthorId, ROLES_ADMIN))
            throw new NoRightsException();

        mapper.Map(dto, comment);

        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);
    }

    [HttpPost]
    public async Task Delete(int id)
    {
        var comment =
            await
                context
                    .Comments
                    .Where(i => i.Id == id)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанный комментарий.");

        if (!await userService.OwnsOrInRole(User, comment.AuthorId, ROLES_ADMIN))
            throw new NoRightsException();

        comment.IsDeleted = true;

        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);
    }
}