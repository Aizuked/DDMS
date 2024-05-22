using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core;
using Core.Dto.Facets;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Miscellaneous.Extensions;
using Web.Services.Identity;
using Web.Viewmodels;
using Web.Viewmodels.Facets;

namespace Web.Areas.Facet;

public class FacetItemController(DdmsDbContext context, UserService userService, IMapper mapper, IToastifyService toastify) : Controller
{
    public async Task<IActionResult> List(ListBaseFilter filter)
    {
        var facetItemsQuery =
            context
                .FacetItems;

        var facetItemsDtos =
            await
                facetItemsQuery
                    .ProjectTo<FacetItemListDto>(mapper.ConfigurationProvider)
                    .Filter(filter)
                    .ToListAsync();

        var viewModel = new FacetItemListViewModel
        {
            PageCount = await facetItemsQuery.CountAsync(),
            CurrentPage = filter.CurrentPage,
            PageSize = filter.PageSize,
            FacetItemListDtos = facetItemsDtos
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (!User.IsInRole(ROLES_ADMIN))
            throw new NoRightsException();

        var facetItem =
            await
                context
                    .FacetItems
                    .Where(i => i.Id == id)
                    .ProjectTo<FacetItemEditDto>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанный элемент справочника.");

        var viewModel = new FacetItemEditViewModel
        {
            FacetItemEditDto = facetItem
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task Edit([FromRoute] int id, [FromBody] FacetEditDto dto)
    {
        if (!User.IsInRole(ROLES_ADMIN))
            throw new NoRightsException();

        var facetItem =
            await
                context
                    .FacetItems
                    .Where(i => i.Id == id)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанный элемент справочника.");

        mapper.Map(dto, facetItem);
        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);
    }
}