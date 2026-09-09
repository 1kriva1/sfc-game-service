using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Entities.Request.Data;
public class RequestStatus : EnumDataEntity<RequestStatusEnum>
{
    public RequestStatus() : base() { }

    public RequestStatus(RequestStatusEnum enumType) : base(enumType) { }
}