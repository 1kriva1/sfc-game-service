using SFC.Game.Application.Common.Dto.Common;
using SFC.Game.Application.Common.Dto.Team.General;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Common.Dto;
using SFC.Game.Domain.Entities.Game.Team.General;

namespace SFC.Game.Application.Features.Game.Team.General.Common.Dto;
public class GameTeamDto : AuditableDto, IMapFrom<GameTeam>
{
    public long Id { get; set; }

    public Guid UserId { get; set; }

    public long TeamId { get; set; }

    public long GameId { get; set; }

    public int StatusId { get; set; }

    public int? Index { get; set; }

    public TeamDto? Team { get; set; }

    public IEnumerable<GameTeamPlayerDto>? Players { get; set; }
}