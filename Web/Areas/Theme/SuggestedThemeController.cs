using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core;
using Core.Dto.Themes;
using Core.Exceptions;
using Core.Models.Themes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Miscellaneous.Extensions;
using Web.Services.Identity;
using Web.Viewmodels;
using Web.Viewmodels.Themes;

namespace Web.Areas.Theme;

public class SuggestedThemeController(DdmsDbContext context, UserService userService, IMapper mapper, IToastifyService toastify) : Controller
{
    public async Task<IActionResult> List(ListPaginationFilter filter)
    {
        var userId = (await userService.GetCurrentOrThrow(User)).Id;

        var suggestedThemeQuery =
            context
                .SuggestedThemes
                .Where(i => i.UserId == userId)
                .AsQueryable();

        var suggestedThemeListDtos =
            await
                suggestedThemeQuery
                    .ProjectTo<SuggestedThemeListDto>(mapper.ConfigurationProvider)
                    .Paginate(filter)
                    .ToListAsync();

        var viewModel = new SuggestedThemeListViewModel
        {
            PageCount = await suggestedThemeQuery.CountAsync(),
            CurrentPage = filter.CurrentPage,
            PageSize = filter.PageSize,
            SuggestedThemeListDtos = suggestedThemeListDtos
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        SuggestedThemeEditDto suggestedTheme = new();

        if (id.HasValue)
            suggestedTheme =
                await
                    context
                        .SuggestedThemes
                        .Where(i => i.Id == id)
                        .ProjectTo<SuggestedThemeEditDto>(mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync()
                ?? throw new NotifiableException("Не удалось найти указанную тему.");

        if (!await userService.OwnsOrInRole(User, suggestedTheme.UserId, ROLES_TEACHER) && suggestedTheme.Id != default)
            throw new NoRightsException();

        var viewModel = new SuggestedThemeEditViewModel
        {
            SuggestedThemeEditDto = suggestedTheme
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<RedirectToActionResult> Edit(SuggestedThemeEditViewModel vm)
    {
        var dto = vm.SuggestedThemeEditDto;
        SuggestedTheme suggestedTheme = new()
        {
            UserId = (await userService.GetCurrentOrThrow(User)).Id
        };

        if (dto.Id != default)
            suggestedTheme =
                await
                    context
                        .SuggestedThemes
                        .Where(i => i.Id == dto.Id)
                        .FirstOrDefaultAsync()
                ?? throw new NotifiableException("Не удалось найти указанную тему.");

        if (!await userService.OwnsOrInRole(User, suggestedTheme.UserId, ROLES_TEACHER))
            throw new NoRightsException();

        mapper.Map(dto, suggestedTheme);

        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);

        return RedirectToAction(nameof(List), new { });
    }
}