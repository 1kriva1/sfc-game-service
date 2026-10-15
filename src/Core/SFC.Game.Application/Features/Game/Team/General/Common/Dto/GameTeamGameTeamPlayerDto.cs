using AutoMapper;

using SFC.Game.Application.Common.Dto.Player.General;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.General.Common.Dto;
public class GameTeamGameTeamPlayerDto : IMapFrom<GameTeamPlayer>
{
    public long Id { get; set; }

    public Guid UserId { get; set; }

    public long GameId { get; set; }

    public long TeamId { get; set; }

    public long PlayerId { get; set; }

    public PlayerDto? Player { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<GameTeamPlayer, GameTeamGameTeamPlayerDto>()
                                                   .ForMember(p => p.GameId, d => d.MapFrom(z => z.GameTeam.GameId))
                                                   .ForMember(p => p.TeamId, d => d.MapFrom(z => z.GameTeam.TeamId))
                                                   .ForMember(p => p.PlayerId, d => d.MapFrom(z => z.GamePlayer != null ? z.GamePlayer!.PlayerId : z.TeamPlayer!.PlayerId))
                                                   .ForMember(p => p.Player, d => d.MapFrom(z => z.GamePlayer != null ? z.GamePlayer!.Player : z.TeamPlayer!.Player));
}