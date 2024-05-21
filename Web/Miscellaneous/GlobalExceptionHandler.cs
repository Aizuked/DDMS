using System.Net;
using AspNetCoreHero.ToastNotification.Abstractions;
using Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Web.Miscellaneous;

public class GlobalExceptionHandler : IExceptionHandler, IDisposable
{
    private readonly IServiceScope _serviceScope;

    public GlobalExceptionHandler(IServiceProvider serviceProvider)
    {
        _serviceScope = serviceProvider.CreateScope();
    }

    public ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (cancellationToken.IsCancellationRequested)
            return ValueTask.FromResult(true);

        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var notifyService =
            _serviceScope
                .ServiceProvider
                .GetService<IToastifyService>();

        if (exception is NotifiableException notifiableException)
        {
            notifyService?.Error(notifiableException.Message);
            return ValueTask.FromResult(true);
        }

        notifyService?.Error(
            "Что-то пошло не так. " +
            "Пожалуйста, обратитесь в техническую поддержку"
        );
        return ValueTask.FromResult(true);
    }

    public void Dispose()
    {
        _serviceScope.Dispose();
    }
}