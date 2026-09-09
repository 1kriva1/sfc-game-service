using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Game.Team.General;

namespace SFC.Game.Application.Features.Game.Team.General.Commands.Update;
public class UpdateGameTeamDto : IMapTo<GameTeam>
{
    public long GameId { get; set; }

    public long TeamId { get; set; }

    public int StatusId { get; set; }

    public int? Index { get; set; }
}