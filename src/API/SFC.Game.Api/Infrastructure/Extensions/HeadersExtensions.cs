using System.Text.Encodings.Web;
using System.Text.Json;

using Grpc.Core;

using Microsoft.Extensions.Primitives;

using SFC.Game.Api.Infrastructure.Models.Pagination;
using SFC.Game.Contracts.Headers;
using SFC.Game.Infrastructure.Constants;

namespace SFC.Game.Api.Infrastructure.Extensions;

public static class HeadersExtensions
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public static void AddPaginationHeader(this HttpResponse response, PageMetadataModel metadata)
    {
        response?.Headers.Append(CommonConstants.PaginationHeaderKey, JsonSerializer.Serialize(metadata, WriteOptions));
    }

    public static void AddPaginationHeader(this ServerCallContext context, PaginationHeader metadata)
    {
        Metadata responseHeaders = new()
        {
            { CommonConstants.PaginationHeaderKey, JsonSerializer.Serialize(metadata, WriteOptions) }
        };
        context.WriteResponseHeadersAsync(responseHeaders);
    }

    public static void AddAuditableHeaderIfRequested(this ServerCallContext context, AuditableHeader header)
    {
        Metadata.Entry? auditableHeader = context.RequestHeaders.FirstOrDefault(h =>
            string.Equals(h.Key, CommonConstants.AuditableHeaderKey, StringComparison.OrdinalIgnoreCase));

        if (bool.TryParse(auditableHeader?.Value, out bool result) && result)
        {
            Metadata responseHeaders = new()
            {
                { CommonConstants.AuditableHeaderKey, JsonSerializer.Serialize(header, WriteOptions) }
            };

            context.WriteResponseHeadersAsync(responseHeaders);
        }
    }

    public static string[]? GetIncludes(this HttpRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!request.Headers.TryGetValue(CommonConstants.IncludeHeaderKey, out StringValues values) || StringValues.IsNullOrEmpty(values))
        {
            return null;
        }

        string[] includes = [.. values
            .SelectMany(x => x!.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            .Distinct(StringComparer.OrdinalIgnoreCase)];

        return includes.Length == 0 ? null : includes;
    }
}