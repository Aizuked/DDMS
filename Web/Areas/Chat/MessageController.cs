using AutoMapper;
using Core;
using Core.Dto.Chats;
using Core.Exceptions;
using Core.Models.Chats;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Services.Identity;

namespace Web.Areas.Chat;

public class MessageController(DdmsDbContext context, UserService userService, IMapper mapper) : Controller
{
    public async Task<JsonResult> Add(MessageEditDto dto)
    {
        var message = new Message
        {
            Content = dto.Content,
            TimeStamp = DateTime.Now,
            LocalFileId = dto.LocalFileId,
            ProjectTaskId = dto.ProjectTaskId,
            SenderId = (await userService.GetCurrentOrThrow(User)).Id,
        };

        return Json(new
            {
                Id = message.Id
            }
        );
    }

    public async Task<JsonResult> Delete(int messageId)
    {
        var message =
            await
                context
                    .Messages
                    .Where(i => i.Id == messageId)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанное сообщение.");

        if (!await userService.OwnsOrInRole(User, message.SenderId, ROLES_ADMIN))
            throw new NoRightsException();

        message.IsDeleted = true;

        await context.SaveChangesAsync();

        return Json(new
            {
                Id = message.Id
            }
        );
    }
}