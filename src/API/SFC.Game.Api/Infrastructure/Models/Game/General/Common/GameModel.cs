using AutoMapper;

using SFC.Game.Api.Infrastructure.Models.Game.Player.Common;
using SFC.Game.Api.Infrastructure.Models.Game.Team.General.Common;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Common.Dto;

namespace SFC.Game.Api.Infrastructure.Models.Game.General.Common;

/// <summary>
/// Game model.
/// </summary>
public class GameModel : BaseGameModel, IMapFrom<GameDto>
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Game status.
    /// </summary>
    public int Status { get; set; }

    public IEnumerable<GamePlayerModel>? Players { get; set; }

    public IEnumerable<GameTeamModel>? Teams { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<GameDto, GameModel>()
                                                   .ForMember(p => p.Status, d => d.MapFrom(z => z.StatusId));
}