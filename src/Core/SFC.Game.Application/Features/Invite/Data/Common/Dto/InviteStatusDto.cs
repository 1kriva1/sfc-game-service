using SFC.Game.Application.Common.Dto.Data;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Invite.Data;

namespace SFC.Game.Application.Features.Invite.Data.Common.Dto;
public class InviteStatusDto : DataDto, IMapTo<InviteStatus> { }