using SFC.Game.Application.Common.Dto.Common;
using SFC.Identity.Contracts.Headers;

namespace SFC.Game.Infrastructure.Extensions;
public static class AuditableExtensions
{
    public static void SetAuditableProperties(this AuditableDto value, AuditableHeader header)
    {
        value.CreatedDate = header.CreatedDate.ToDateTime();
        value.CreatedBy = Guid.Parse(header.CreatedBy);
        value.LastModifiedDate = header.LastModifiedDate.ToDateTime();
        value.LastModifiedBy = Guid.Parse(header.LastModifiedBy);
    }
}