using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core;
using Core.Dto.Themes;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Miscellaneous.Extensions;
using Web.Services.Identity;
using Web.Viewmodels;
using Web.Viewmodels.Themes;

namespace Web.Areas.Theme;

public class KeyWordController(DdmsDbContext context, UserService userService, IMapper mapper, IToastifyService toastify) : Controller
{
    public async Task<IActionResult> List(string term, ListPaginationFilter filter)
    {
        if (
            !User.IsInRole(ROLES_ADMIN) &&
            !User.IsInRole(ROLES_TEACHER)
        )
            throw new NoRightsException();

        var keyWordsQuery =
            context
                .KeyWords
                .AsQueryable();

        // TODO: vectorize
        if (!string.IsNullOrEmpty(term))
            keyWordsQuery =
                keyWordsQuery
                    .Where(i => i.Word.Contains(term));

        var keyWordsListDtos =
            await
                keyWordsQuery
                    .ProjectTo<KeyWordListDto>(mapper.ConfigurationProvider)
                    .Paginate(filter)
                    .ToListAsync();

        var viewModel = new KeyWordListListViewModel
        {
            PageCount = await keyWordsQuery.CountAsync(),
            CurrentPage = filter.CurrentPage,
            PageSize = filter.PageSize,
            KeyWordListDtos = keyWordsListDtos
        };

        return View(viewModel);
    }

    public async Task<JsonResult> List(string? term)
    {
        var filter = new ListPaginationFilter();

        var keyWordsQuery =
            context
                .KeyWords
                .Where(i => i.IsApproved)
                .AsQueryable();

        // TODO: vectorize
        if (!string.IsNullOrEmpty(term))
            keyWordsQuery =
                keyWordsQuery
                    .Where(i => i.Word.Contains(term));

        var keyWordsListDtos =
            await
                keyWordsQuery
                    .ProjectTo<KeyWordListDto>(mapper.ConfigurationProvider)
                    .Paginate(filter)
                    .ToListAsync();

        return new JsonResult(keyWordsListDtos);
    }

    [HttpPost]
    public async Task<RedirectToActionResult> ToggleApprove(int id)
    {
        if (
            !User.IsInRole(ROLES_ADMIN) &&
            !User.IsInRole(ROLES_TEACHER)
        )
            throw new NoRightsException();

        var updated =
            1 ==
            await
                context
                    .KeyWords
                    .Where(i => i.Id == id)
                    .ExecuteUpdateAsync(
                        i =>
                        i.SetProperty(
                            p => p.IsApproved,
                            p => !p.IsApproved
                        )
                    );

        if (!updated)
            throw new NotifiableException("Не удалось найти указанное ключевое слово.");

        toastify.Success(NOTIFY_SUCCESS);

        return RedirectToAction(nameof(List), new { });
    }
}