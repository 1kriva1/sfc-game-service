using System.Net;
using System.Text.Json;

using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Infrastructure.Constants;

using ExceptionType = System.Exception;

namespace SFC.Game.Api.Infrastructure.Middlewares;

using Handler = Func<ExceptionType, ExceptionResponse>;

internal record struct ExceptionResponse(HttpStatusCode StatusCode, BaseResponse Result) { }

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    private readonly Dictionary<Type, Handler> _exceptionHandlers;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _exceptionHandlers = new Dictionary<Type, Handler>
        {
            { typeof(BadRequestException), HandleBadRequestException },
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(AuthorizationException), HandleAuthorizationException },
            { typeof(ConfigurationException), HandleInternalException },
            { typeof(TokenExchangeException), HandleInternalException },
            { typeof(CommunicationException), HandleInternalException },
            { typeof(TimeOutException), HandleTimeOutException },
            { typeof(ConflictException), HandleConflictException },
        };
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        try
        {
            await _next(context).ConfigureAwait(false);
        }
        catch (ExceptionType ex)
        {
            await HandleExceptionAsync(context, ex).ConfigureAwait(false);
            throw;
        }
    }

    private Task HandleExceptionAsync(HttpContext context, ExceptionType exception)
    {
        Type exceptionType = exception.GetType();

        ExceptionResponse response = _exceptionHandlers.TryGetValue(exceptionType, out Handler? handler)
            ? handler.Invoke(exception)
            : HandleInternalException(exception);

        context.Response.StatusCode = (int)response.StatusCode;

        context.Response.ContentType = context.Request.ContentType ?? CommonConstants.JsonContentType;

        return context.Response.WriteAsync(JsonSerializer.Serialize(response.Result));
    }

    private ExceptionResponse HandleBadRequestException(ExceptionType exception)
    {
        Dictionary<string, IEnumerable<string>> validationErrors = ((BadRequestException)exception).Errors;

        return new ExceptionResponse(HttpStatusCode.BadRequest, new BaseErrorResponse(exception.Message, validationErrors));
    }

    private ExceptionResponse HandleNotFoundException(ExceptionType exception)
    {
        return new ExceptionResponse(HttpStatusCode.NotFound, new BaseResponse(exception.Message, false));
    }

    private ExceptionResponse HandleAuthorizationException(ExceptionType exception)
    {
        return new ExceptionResponse(HttpStatusCode.Unauthorized, new BaseResponse(Localization.AuthorizationError, false));
    }

    private ExceptionResponse HandleInternalException(ExceptionType exception)
    {
        return new(HttpStatusCode.InternalServerError, new BaseResponse(Localization.FailedResult, false));
    }

    private ExceptionResponse HandleTimeOutException(ExceptionType exception)
    {
        return new(HttpStatusCode.GatewayTimeout, new BaseResponse(Localization.TimeOutError, false));
    }
    private static ExceptionResponse HandleConflictException(ExceptionType exception)
    {
        return new ExceptionResponse(HttpStatusCode.Conflict, new BaseResponse(exception.Message, false));
    }
}