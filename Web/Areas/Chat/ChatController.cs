using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core;
using Core.Dto.Chats;
using Core.Exceptions;
using Core.Models.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Miscellaneous.Extensions;
using Web.Services.Identity;
using Web.Viewmodels;
using Web.Viewmodels.Chats;

namespace Web.Areas.Chat;

public class ChatController(DdmsDbContext context, UserService userService, IMapper mapper, IToastifyService toastify) : Controller
{
    public async Task<IActionResult> List(ListPaginationFilter filter, int page, int? chatId)
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
                //.OrderByDescending(i => i.Messages.Select(j => j.TimeStamp));

        var chatListDtos =
            await
                chatQuery
                    .ProjectTo<ChatListDto>(mapper.ConfigurationProvider)
                    .Paginate(filter)
                    .ToListAsync();

        chatId ??=
            await
                chatQuery
                    .Select(i => i.Id)
                    .FirstOrDefaultAsync();

        var messages =
            await
                context
                    .Chats
                    .Where(i => i.Id == chatId)
                    .SelectMany(i => i.Messages)
                    .Skip(filter.PageSize * page)
                    .Take(filter.PageSize)
                    .ProjectTo<MessageListDto>(mapper.ConfigurationProvider)
                    .ToListAsync();

        var viewModel = new ChatListViewModel
        {
            PageCount = await chatQuery.CountAsync(),
            CurrentPage = filter.CurrentPage,
            PageSize = filter.PageSize,
            ChatListDtos = chatListDtos,
            MessageListDtos = messages,
            SelfId = userId
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        ChatEditDto chat = new();
        if (id.HasValue)
            chat =
                await
                    context
                        .Chats
                        .Where(i => i.Id == id)
                        .ProjectTo<ChatEditDto>(mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync()
                ?? throw new NotifiableException("Не удалось найти указанный чат.");

        var userId = (await userService.GetCurrentOrThrow(User)).Id;

        if (!chat.ParticipantIds.Contains(userId) && chat.Id != default)
            throw new NoRightsException();

        var viewModel = new ChatEditViewModel
        {
            ChatEditDto = chat
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<RedirectToActionResult> Edit(ChatEditViewModel vm)
    {
        var user = await userService.GetCurrentOrThrow(User);

        var dto = vm.ChatEditDto;
        Core.Models.Chats.Chat chat = new()
        {
            Participants = new List<User>{ user }
        };

        if (dto.Id != default)
            chat =
                await
                    context
                        .Chats
                        .Where(i => i.Id == dto.Id)
                        .Include(i => i.Participants)
                        .FirstOrDefaultAsync()
                ?? throw new NotifiableException("Не удалось найти указанный чат.");

        var userId = (await userService.GetCurrentOrThrow(User)).Id;

        if (!chat.Participants.Select(j => j.Id).Contains(userId))
            throw new NoRightsException();

        mapper.Map(dto, chat);
        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);

        return RedirectToAction(nameof(List), new { });
    }

    [HttpPost]
    public async Task<RedirectToActionResult> Delete(int id)
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

        return RedirectToAction(nameof(List), new { });
    }
}