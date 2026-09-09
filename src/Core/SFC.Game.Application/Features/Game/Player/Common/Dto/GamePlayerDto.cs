using SFC.Game.Application.Common.Dto.Common;
using SFC.Game.Application.Common.Dto.Player.General;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.General.Common.Dto;
using SFC.Game.Domain.Entities.Game.Player;

namespace SFC.Game.Application.Features.Game.Player.Common.Dto;
public class GamePlayerDto : AuditableDto, IMapFrom<GamePlayer>
{
    public long Id { get; set; }

    public Guid UserId { get; set; }

    public long GameId { get; set; }

    public long PlayerId { get; set; }

    public int StatusId { get; set; }

    public PlayerDto? Player { get; set; }

    public GameTeamDto? GameTeam { get; set; }
}