using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core;
using Core.Dto.Files;
using Core.Exceptions;
using Core.Models.Files;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeMapping;
using Web.Miscellaneous.Extensions;
using Web.Services.Identity;
using Web.Viewmodels;
using Web.Viewmodels.Files;

namespace Web.Areas.Files;

public class LocalFileController(DdmsDbContext context, UserService userService, IMapper mapper, IConfiguration configuration, IToastifyService toastify) : Controller
{
    public async Task<IActionResult> List(ListPaginationFilter filter)
    {
        if (!User.IsInRole(ROLES_ADMIN))
            throw new NoRightsException();

        var filesQuery =
            context
                .LocalFiles
                .Where(i => !i.IsDeleted)
                .AsQueryable();

        var userDtos =
            await
                filesQuery
                    .ProjectTo<LocalFileListDto>(mapper.ConfigurationProvider)
                    .Paginate(filter)
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

    [HttpPost]
    public async Task Add(int fileGroupId, IFormFileCollection fileCollection)
    {
        var user =
            await
                userService
                    .GetCurrentOrThrow(User);

        var fileGroup =
            await
                context
                    .LocalFileGroups
                    .Where(i => i.Id == fileGroupId)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанную группу файлов.");

        var localFiles = new List<LocalFile>();

        foreach (var file in fileCollection)
        {
            var mime =
                MimeUtility
                    .GetMimeMapping(file.FileName);

            if (!fileGroup.AllowedMimeTypes.Contains(mime))
                continue;

            IFileProcessor fileProcessor =
                mime.Contains("image/")
                    ? new ImageFileProcessor(configuration)
                    : new FileProcessor();

            var fileService = new FileSystemService(
                configuration,
                fileProcessor
            );

            var filePath =
                await fileService.StoreAsync(
                    file.OpenReadStream(),
                    MimeUtility.GetExtensions(mime).Aggregate((a, b) => a + b),
                    user.Id
                );

            var localFile = new LocalFile
            {
                PhysicalPath = filePath,
                DisplayName = file.Name,
                MimeType = mime,
                Size = file.Length,
                UploaderId = user.Id,
                LocalFileGroupId = fileGroupId
            };

            localFiles.Add(localFile);
        }

        context.AddRange(localFiles);
        await context.SaveChangesAsync();

        if (localFiles.Count != fileCollection.Count)
            throw new NotifiableException($"{fileCollection.Count - localFiles.Count}.");

        toastify.Success(NOTIFY_SUCCESS);
    }

    [HttpPost]
    public async Task Delete(int id)
    {
        var localFile =
            await
                context
                    .LocalFiles
                    .Where(i => i.Id == id)
                    .FirstOrDefaultAsync()
            ?? throw new NotifiableException("Не удалось найти указанный файл.");

        if (!await userService.OwnsOrInRole(User, localFile.UploaderId, ROLES_ADMIN))
            throw new NoRightsException();

        localFile.IsDeleted = true;

        await context.SaveChangesAsync();

        toastify.Success(NOTIFY_SUCCESS);
    }
}
