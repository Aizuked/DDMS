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

public class LocalFileController(DdmsDbContext context, UserService userService, IMapper mapper) : Controller
{
    public async Task<IActionResult> List(ListBaseFilter filter)
    {
        if (!User.IsInRole(ROLES_ADMIN))
            throw new NoRightsException();

        var filesQuery = 
            context
                .LocalFiles;

        var userDtos =
            await
                filesQuery
                    .ProjectTo<LocalFileListDto>(mapper.ConfigurationProvider)
                    .Filter(filter)
                    .ToListAsync();

        var viewModel = new LocalFileListViewModel
        {
            PageCount = await filesQuery.CountAsync(),
            CurrentPage = filter.CurrentPage,
            PageSize = filter.PageSize,
            LocalFileListDtos = userDtos
        };

        return View(viewModel);
    }
}