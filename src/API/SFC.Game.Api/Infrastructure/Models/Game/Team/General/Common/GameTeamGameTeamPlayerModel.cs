using SFC.Game.Api.Infrastructure.Models.Player;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.General.Common.Dto;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.General.Common;

/// <summary>
/// Game Team Player model.
/// </summary>
public class GameTeamGameTeamPlayerModel : IMapFrom<GameTeamGameTeamPlayerDto>
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Game Team Player related to this player.
    /// </summary>
    public PlayerModel? Player { get; set; }
}