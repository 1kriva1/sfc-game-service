using SFC.Game.Application.Common.Dto.Common;
using SFC.Game.Application.Common.Dto.Player.General;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Team.Player;

namespace SFC.Game.Application.Common.Dto.Team.Player;
public class TeamPlayerDto : AuditableDto, IMapFromReverse<TeamPlayer>
{
    public long Id { get; set; }

    public long TeamId { get; set; }

    public long PlayerId { get; set; }

    public int StatusId { get; set; }

    public Guid UserId { get; set; }

    public PlayerDto? Player { get; set; }
}