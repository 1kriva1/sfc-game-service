using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Commands.Deletes;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Deletes;

/// <summary>
/// **Updates** game team player models.
/// </summary>
public class DeletesGameTeamPlayerModel : IMapTo<DeletesGameTeamPlayerDto>
{
    /// <summary>
    /// Player unique identifier.
    /// </summary>
    public long Player { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<DeletesGameTeamPlayerModel, DeletesGameTeamPlayerDto>()
                                                   .ForMember(p => p.PlayerId, d => d.MapFrom(z => z.Player));
}