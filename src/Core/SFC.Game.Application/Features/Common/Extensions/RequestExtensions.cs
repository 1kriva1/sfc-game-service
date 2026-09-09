using SFC.Game.Application.Features.Common.Base;

namespace SFC.Game.Application.Features.Common.Extensions;

public static class RequestExtensions
{
    public static TRequest SetIncludes<TRequest>(this TRequest request, IEnumerable<string>? includes)
        where TRequest : BaseRequest
    {
        ArgumentNullException.ThrowIfNull(request);

        request.Includes = includes?.Distinct(StringComparer.OrdinalIgnoreCase).ToArray();
        return request;
    }
}