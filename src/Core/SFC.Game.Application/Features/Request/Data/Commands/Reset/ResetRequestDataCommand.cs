using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Request.Data.Common.Dto;

namespace SFC.Game.Application.Features.Request.Data.Commands.Reset;
public class ResetRequestDataCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.ResetRequestData; }

    public IEnumerable<RequestStatusDto> RequestStatuses { get; init; } = [];
}