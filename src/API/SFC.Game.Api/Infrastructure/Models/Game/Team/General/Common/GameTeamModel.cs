using AutoMapper;

using SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Common;
using SFC.Game.Api.Infrastructure.Models.Team.General;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.General.Common.Dto;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.General.Common;

/// <summary>
/// Game Team model.
/// </summary>
public class GameTeamModel : IMapFrom<GameTeamDto>
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Game Team status.
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Game Team index.
    /// </summary>
    public int? Index { get; set; }

    /// <summary>
    /// Game Team related to this team.
    /// </summary>
    public TeamModel? Team { get; set; }

    /// <summary>
    /// Game Team related to this team.
    /// </summary>
    public IEnumerable<GameTeamPlayerModel>? Players { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<GameTeamDto, GameTeamModel>()
                                                   .ForMember(p => p.Status, d => d.MapFrom(z => z.StatusId));
}