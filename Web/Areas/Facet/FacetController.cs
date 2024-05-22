﻿using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core;
using Core.Dto.Facets;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Services.Identity;
using Web.Viewmodels.Facets;

namespace Web.Areas.Facet;

public class FacetController(DdmsDbContext context, UserService userService, IMapper mapper, IToastifyService toastify) : Controller
{
    public async Task<IActionResult> Edit(int id)
    {
        if (!User.IsInRole(ROLES_ADMIN))
            throw new NoRightsException();

        var facet =
            await
                context
                    .Facets
                    .Where(i => i.Id == id)
                    .ProjectTo<FacetEditDto>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync()
                ?? throw new NotifiableException("Не удалось найти указанный справочник.");

        var viewModel = new FacetEditViewModel
        {
            FacetEditDto = facet
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task Edit([FromRoute] int id, [FromBody] FacetEditDto dto)
    {
        if (!User.IsInRole(ROLES_ADMIN))
            throw new NoRightsException();

        var facet =
            await
                context
                    .Facets
                    .Where(i => i.Id == id)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанный справочник.");

        mapper.Map(dto, facet);
        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);
    }
}