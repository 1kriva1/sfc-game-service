using AutoMapper;

using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.Team.General.Common;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.General.Queries.Get;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.General.Get;

/// <summary>
/// **Get** game team response.
/// </summary>
public class GetGameTeamResponse :
    BaseErrorResponse, IMapFrom<GetGameTeamViewModel>
{
    /// <summary>
    /// Game team model.
    /// </summary>
    public GameTeamModel GameTeam { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<GetGameTeamViewModel, GetGameTeamResponse>()
                                                   .IgnoreAllNonExisting();
}