using AutoMapper;

using SFC.Game.Application.Common.Dto.Player.General;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.General.Common.Dto;
using SFC.Game.Domain.Entities.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Common.Dto;
public class GameTeamPlayerDto : IMapFrom<GameTeamPlayer>
{
    public long Id { get; set; }

    public Guid UserId { get; set; }

    public long GameTeamId { get; set; }

    public long GameId { get; set; }

    public long TeamId { get; set; }

    public long PlayerId { get; set; }

    public int StatusId { get; set; }

    public PlayerDto? Player { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<GameTeamPlayer, GameTeamPlayerDto>()
                                                   .ForMember(p => p.GameId, d => d.MapFrom(z => z.GameTeam.GameId))
                                                   .ForMember(p => p.TeamId, d => d.MapFrom(z => z.GameTeam.TeamId));
}