using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Commands.Creates;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Creates;

/// <summary>
/// **Creates** game team player model.
/// </summary>
public class CreatesGameTeamPlayerModel : IMapTo<CreatesGameTeamPlayerDto>
{
    public long Player { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<CreatesGameTeamPlayerModel, CreatesGameTeamPlayerDto>()
                                                   .ForMember(p => p.PlayerId, d => d.MapFrom(z => z.Player));
}