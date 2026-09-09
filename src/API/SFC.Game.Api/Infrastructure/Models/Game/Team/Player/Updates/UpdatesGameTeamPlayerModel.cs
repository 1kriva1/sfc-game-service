using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Commands.Updates;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Updates;

/// <summary>
/// **Updates** game team player models.
/// </summary>
public class UpdatesGameTeamPlayerModel : IMapTo<UpdatesGameTeamPlayerDto>
{
    /// <summary>
    /// Status of game team player.
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Player unique identifier.
    /// </summary>
    public long Player { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<UpdatesGameTeamPlayerModel, UpdatesGameTeamPlayerDto>()
                                                   .ForMember(p => p.StatusId, d => d.MapFrom(z => z.Status))
                                                   .ForMember(p => p.PlayerId, d => d.MapFrom(z => z.Player));
}