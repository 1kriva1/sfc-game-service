using AutoMapper;

using SFC.Game.Api.Infrastructure.Models.Game.Team.General.Common;
using SFC.Game.Api.Infrastructure.Models.Player;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Player.Common.Dto;

namespace SFC.Game.Api.Infrastructure.Models.Game.Player.Common;

/// <summary>
/// Game Player model.
/// </summary>
public class GamePlayerModel : IMapFrom<GamePlayerDto>
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Game Player status.
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Game Player related to this player.
    /// </summary>
    public PlayerModel? Player { get; set; }

    /// <summary>
    /// Game Team related to this game player.
    /// </summary>
    public GameTeamModel? GameTeam { get; set; }


    public void Mapping(Profile profile) => profile.CreateMap<GamePlayerDto, GamePlayerModel>()
                                                   .ForMember(p => p.Status, d => d.MapFrom(z => z.StatusId));
}