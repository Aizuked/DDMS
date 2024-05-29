using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core;
using Core.Dto.Chats;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Miscellaneous.Extensions;
using Web.Services.Identity;
using Web.Viewmodels;
using Web.Viewmodels.Chats;

namespace Web.Areas.Chat;

public class ChatController(DdmsDbContext context, UserService userService, IMapper mapper, IToastifyService toastify) : Controller
{
    public async Task<IActionResult> List(ListPaginationFilter filter)
    {
        var chatQuery =
            context
                .Chats
                .Where(i => !i.IsDeleted)
                .AsQueryable();

        var userId = (await userService.GetCurrentOrThrow(User)).Id;

        chatQuery =
            chatQuery
                .Where(i => i.Participants.Select(j => j.Id).Contains(userId));

        var chatListDtos =
            await
                chatQuery
                    .ProjectTo<ChatListDto>(mapper.ConfigurationProvider)
                    .Paginate(filter)
                    .ToListAsync();

        var viewModel = new ChatListViewModel
        {
            PageCount = await chatQuery.CountAsync(),
            CurrentPage = filter.CurrentPage,
            PageSize = filter.PageSize,
            ChatListDtos = chatListDtos
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var chat =
            await
                context
                    .Chats
                    .Where(i => i.Id == id)
                    .ProjectTo<ChatEditDto>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанный чат.");

        var userId = (await userService.GetCurrentOrThrow(User)).Id;

        if (!chat.ParticipantIds.Contains(userId))
            throw new NoRightsException();

        var viewModel = new ChatEditViewModel
        {
            ChatEditDto = chat
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task Edit(ChatEditDto dto)
    {
        var chatQuery =
            context
                .Chats
                .Where(i => i.Id == dto.Id);

        var userId = (await userService.GetCurrentOrThrow(User)).Id;

        if (chatQuery.All(i => !i.Participants.Select(j => j.Id).Contains(userId)))
            throw new NoRightsException();

        var chat =
            await
                chatQuery
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанный чат.");

        mapper.Map(dto, chat);
        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);
    }

    [HttpPost]
    public async Task Delete(int id)
    {
        var chatQuery =
            context
                .Chats
                .Where(i => i.Id == id);

        var userId = (await userService.GetCurrentOrThrow(User)).Id;

        if (chatQuery.All(i => !i.Participants.Select(j => j.Id).Contains(userId)))
            throw new NoRightsException();

        var chat =
            await
                chatQuery
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанный чат.");

        chat.IsDeleted = true;

        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);
    }
}