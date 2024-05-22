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
    public async Task<IActionResult> List(ListBaseFilter filter)
    {
        if (
            !User.IsInRole(ROLES_ADMIN) ||
            !User.IsInRole(ROLES_TEACHER)
        )
            throw new NoRightsException();

        var fileGroupQuery =
            context
                .LocalFileGroups;

        var fileGroupDtos =
            await
                fileGroupQuery
                    .ProjectTo<LocalFileGroupListDto>(mapper.ConfigurationProvider)
                    .Filter(filter)
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

    public async Task<IActionResult> Edit(int id)
    {
        if (
            !User.IsInRole(ROLES_ADMIN) ||
            !User.IsInRole(ROLES_TEACHER)
        )
            throw new NoRightsException();

        var localFileGroupEditDto =
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
    public async Task Edit([FromRoute] int id, [FromBody] LocalFileGroupEditDto dto)
    {
        if (
            !User.IsInRole(ROLES_ADMIN) ||
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

        mapper.Map(dto, localFileGroup);
        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);
    }
}