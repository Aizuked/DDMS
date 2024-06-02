using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core;
using Core.Dto.Identity;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Miscellaneous.Extensions;
using Web.Services.Identity;
using Web.Viewmodels;
using Web.Viewmodels.Identity;

namespace Web.Areas.Identity;

public class UserController(DdmsDbContext context, UserService userService, IMapper mapper, IToastifyService toastify) : Controller
{
    public async Task<IActionResult> List(ListPaginationFilter filter)
    {
        var usersQuery =
            userService
                .GetUserQuery();

        var userDtos =
            await
                usersQuery
                    .ProjectTo<UserListDto>(mapper.ConfigurationProvider)
                    .Paginate(filter)
                    .ToListAsync();

        var viewModel = new UserListViewModel
        {
            PageCount = await usersQuery.CountAsync(),
            CurrentPage = filter.CurrentPage,
            PageSize = filter.PageSize,
            UserListDtos = userDtos
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var viewModel = new UserDetailsViewModel
        {
            CanEdit = await userService.OwnsOrInRole(User, id, ROLES_ADMIN),
            CanModifyRoles = User.IsInRole(ROLES_ADMIN),
            UserDetailsDto = mapper.Map<UserDetailsDto>(await userService.UserByIdOrThrow(id))
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (!await userService.OwnsOrInRole(User, id, ROLES_ADMIN))
            throw new NoRightsException();

        var viewModel = new UserEditViewModel
        {
            UserEditDto = mapper.Map<UserEditDto>(await userService.UserByIdOrThrow(id))
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task Edit(UserEditDto dto)
    {
        if (!await userService.OwnsOrInRole(User, dto.Id, ROLES_ADMIN))
            throw new NoRightsException();

        var user = await userService.UserByIdOrThrow(dto.Id);

        mapper.Map(dto, user);
        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);
    }
}