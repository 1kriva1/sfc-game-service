using AutoMapper;

using SFC.Game.Api.Infrastructure.Models.Team.Player;
using SFC.Game.Application.Common.Dto.Team.General;
using SFC.Game.Application.Common.Mappings.Interfaces;

namespace SFC.Game.Api.Infrastructure.Models.Team.General;

/// <summary>
/// Team model.
/// </summary>
public class TeamModel : IMapFrom<TeamDto>
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Team status.
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Team's profile model.
    /// </summary>
    public TeamProfileModel? Profile { get; set; }

    /// <summary>
    /// Team's players.
    /// </summary>
    public IEnumerable<TeamPlayerModel>? Players { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<TeamDto, TeamModel>()
                                                  .ForMember(p => p.Status, d => d.MapFrom(z => z.StatusId));
}