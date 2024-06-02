using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core;
using Core.Dto.Chats;
using Core.Exceptions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Web.Services.Identity;

namespace Web.Services.Chat;

public class ChatHub(DdmsDbContext context, IMapper mapper, UserService userService) : Hub
{
    public async Task SendMessage(int chatId, int messageId)
    {
        var msgDto =
            await
                context
                    .Messages
                    .Where(i => i.Id == messageId)
                    .ProjectTo<MessageListDto>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось отправить сообщение.");

        var userData = await
            (
                from u in context.Users
                join f in context.LocalFiles on u.ProfilePictureId equals f.Id
                select new
                {
                    u.Id,
                    f.PhysicalPath
                }
            ).FirstOrDefaultAsync();

        msgDto.ChatId = chatId;
        msgDto.SenderDetailsPath = $"/Identity/User/Details{userData?.Id}";
        msgDto.SenderProfilePicturePath = userData?.PhysicalPath;

        await Clients.All.SendAsync(
            "ReceiveMessage",
            msgDto
        );
    }

    public override Task OnConnectedAsync()
    {
        userService
            .TryToggleOnline(Context.User)
            .ConfigureAwait(false);

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        userService
            .TryToggleOnline(Context.User)
            .ConfigureAwait(false);

        return base.OnDisconnectedAsync(exception);
    }
}
