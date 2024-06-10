using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core;
using Core.Dto.Files;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Miscellaneous.Extensions;
using Web.Services.Identity;
using Web.Viewmodels;
using Web.Viewmodels.Files;

namespace Web.Areas.Files;

public class LocalFileGroupController(DdmsDbContext context, UserService userService, IMapper mapper, IToastifyService toastify) : Controller
{
    public async Task<IActionResult> List(ListPaginationFilter filter)
    {
        if (
            !User.IsInRole(ROLES_ADMIN) &&
            !User.IsInRole(ROLES_TEACHER)
        )
            throw new NoRightsException();

        var fileGroupQuery =
            context
                .LocalFileGroups
                .Where(i => !i.IsDeleted)
                .AsQueryable();

        var fileGroupDtos =
            await
                fileGroupQuery
                    .ProjectTo<LocalFileGroupListDto>(mapper.ConfigurationProvider)
                    .Paginate(filter)
                    .ToListAsync();

        var viewModel = new LocalFileGroupListViewModel
        {
            PageCount = await fileGroupQuery.CountAsync(),
            CurrentPage = filter.CurrentPage,
            PageSize = filter.PageSize,
            LocalFileGroupListDtos = fileGroupDtos
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (
            !User.IsInRole(ROLES_ADMIN) &&
            !User.IsInRole(ROLES_TEACHER)
        )
            throw new NoRightsException();

        LocalFileGroupEditDto localFileGroupEditDto = new();

        if (id.HasValue)
            localFileGroupEditDto =
                await
                    context
                        .LocalFileGroups
                        .Where(i => i.Id == id)
                        .ProjectTo<LocalFileGroupEditDto>(mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync()
                ?? throw new NotifiableException("Не удалось найти указанную группу файлов.");

        var viewModel = new LocalFileGroupEditViewModel
        {
            LocalFileGroupEditDto = localFileGroupEditDto
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<RedirectToActionResult> Edit(LocalFileGroupEditViewModel vm)
    {
        var dto = vm.LocalFileGroupEditDto;
        if (
            !User.IsInRole(ROLES_ADMIN) &&
            !User.IsInRole(ROLES_TEACHER)
        )
            throw new NoRightsException();

        var localFileGroup =
            await
                context
                    .LocalFileGroups
                    .Where(i => i.Id == dto.Id)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанную группу файлов.");

        mapper.Map(dto, localFileGroup);
        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);

        return RedirectToAction(nameof(List), new { });
    }

    [HttpPost]
    public async Task<RedirectToActionResult> Delete(int id)
    {
        if (
            !User.IsInRole(ROLES_ADMIN) &&
            !User.IsInRole(ROLES_TEACHER)
        )
            throw new NoRightsException();

        var localFileGroup =
            await
                context
                    .LocalFileGroups
                    .Where(i => i.Id == id)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанную группу файлов.");

        localFileGroup.IsDeleted = true;

        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);

        return RedirectToAction(nameof(List), new { });
    }
}