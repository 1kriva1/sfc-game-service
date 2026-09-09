using AutoMapper;

using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Common;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Queries.Get;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Get;

/// <summary>
/// **Get** game team player response.
/// </summary>
public class GetGameTeamPlayerResponse :
    BaseErrorResponse, IMapFrom<GetGameTeamPlayerViewModel>
{
    /// <summary>
    /// Game team player model.
    /// </summary>
    public GameTeamPlayerModel GameTeamPlayer { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<GetGameTeamPlayerViewModel, GetGameTeamPlayerResponse>()
                                                   .IgnoreAllNonExisting();
}