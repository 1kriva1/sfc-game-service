using AutoMapper;

using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.Team.General.Common;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.General.Queries.Gets;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.General.Gets;

/// <summary>
/// **Get** all game teams response.
/// </summary>
public class GetsGameTeamResponse :
    BaseErrorResponse, IMapFrom<GetsGameTeamViewModel>
{
    /// <summary>
    /// Game Team models.
    /// </summary>
    public IEnumerable<GameTeamModel> GameTeams { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<GetsGameTeamViewModel, GetsGameTeamResponse>()
                                                   .IgnoreAllNonExisting();
}