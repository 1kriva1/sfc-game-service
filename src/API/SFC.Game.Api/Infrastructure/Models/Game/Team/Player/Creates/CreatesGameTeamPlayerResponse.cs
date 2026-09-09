using AutoMapper;

using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Common;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Commands.Creates;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Creates;

/// <summary>
/// **Creates** game team player response model.
/// </summary>
public class CreatesGameTeamPlayerResponse :
    BaseErrorResponse, IMapFrom<CreatesGameTeamPlayerViewModel>
{
    /// <summary>
    /// Game team player models.
    /// </summary>
    public IEnumerable<GameTeamPlayerModel> GameTeamPlayers { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<CreatesGameTeamPlayerViewModel, CreatesGameTeamPlayerResponse>()
                                                   .IgnoreAllNonExisting();
}