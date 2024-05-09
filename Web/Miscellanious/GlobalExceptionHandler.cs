using System.Net;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Diagnostics;

namespace Web.Miscellanious;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IToastifyService _notifyService;

    public GlobalExceptionHandler(IToastifyService notifyService)
    {
        _notifyService = notifyService;
    }

    public ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (cancellationToken.IsCancellationRequested)
            return ValueTask.FromResult(false);

        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        _notifyService.Error("Что-то пошло не так. Пожалуйста, обратитесь в техническую поддержку");

        return ValueTask.FromResult(true);
    }
}