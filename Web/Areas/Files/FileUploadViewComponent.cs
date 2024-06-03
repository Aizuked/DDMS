using AutoMapper;
using Core;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Services.Identity;
using Web.Viewmodels.Files;

namespace Web.Areas.Files;

public class FileUploadViewComponent(DdmsDbContext context, UserService userService, IMapper mapper) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(
        int? localFileGroupId
    )
    {
        var model = new LocalFileViewModel();

        if (!localFileGroupId.HasValue)
            return await
                Task.FromResult(
                    View(
                        nameof(FileUploadViewComponent),
                        model
                    )
                );

        var fileGroupProperties =
            await
                context
                    .LocalFileGroups
                    .Where(i => i.Id == localFileGroupId)
                    .Select(i => new { i.AllowedMimeTypes, i.MaxSize })
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти укзанную группу файлов!");

        model = new LocalFileViewModel
        {
            MaxSize = fileGroupProperties.MaxSize,
            AllowedMimeTypes = fileGroupProperties.AllowedMimeTypes
        };

        return await
            Task.FromResult(
                View(
                    nameof(FileUploadViewComponent),
                    model
                )
            );
    }
}