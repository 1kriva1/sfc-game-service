using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Commands.Create;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Create;

/// <summary>
/// **Create** game team player model.
/// </summary>
public class CreateGameTeamPlayerModel : IMapTo<CreateGameTeamPlayerDto>
{
    public int? Status { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<CreateGameTeamPlayerModel, CreateGameTeamPlayerDto>()
                                                   .ForMember(p => p.StatusId, d => d.MapFrom(z => z.Status));
}