using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Commands.Update;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Update;

/// <summary>
/// **Update** game team player model.
/// </summary>
public class UpdateGameTeamPlayerModel : IMapTo<UpdateGameTeamPlayerDto>
{
    /// <summary>
    /// Status of game team player.
    /// </summary>
    public int Status { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<UpdateGameTeamPlayerModel, UpdateGameTeamPlayerDto>()
                                                   .ForMember(p => p.StatusId, d => d.MapFrom(z => z.Status));
}