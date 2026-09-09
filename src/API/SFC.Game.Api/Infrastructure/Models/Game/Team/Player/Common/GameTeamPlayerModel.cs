using AutoMapper;

using SFC.Game.Api.Infrastructure.Models.Game.Team.General.Common;
using SFC.Game.Api.Infrastructure.Models.Player;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Common.Dto;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Common;

/// <summary>
/// Game Team Player model.
/// </summary>
public class GameTeamPlayerModel : IMapFrom<GameTeamPlayerDto>
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Game Team Player status.
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Game Team Player related to this player.
    /// </summary>
    public PlayerModel? Player { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<GameTeamPlayerDto, GameTeamPlayerModel>()
                                                   .ForMember(p => p.Status, d => d.MapFrom(z => z.StatusId));
}