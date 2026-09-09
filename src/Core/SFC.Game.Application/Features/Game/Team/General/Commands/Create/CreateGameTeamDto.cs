using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Game.Team.General;

namespace SFC.Game.Application.Features.Game.Team.General.Commands.Create;

public class CreateGameTeamDto : IMapTo<GameTeam>
{
    public long GameId { get; set; }

    public long TeamId { get; set; }

    public int? StatusId { get; set; }

    public int? Index { get; set; }

    public Guid UserId { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<CreateGameTeamDto, GameTeam>()
        .ForMember(dest => dest.StatusId, opt => opt.NullSubstitute(GameTeamStatusEnum.OutOfGame));
}