using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Game.Player;

namespace SFC.Game.Application.Features.Game.Player.Commands.Create;

public class CreateGamePlayerDto : IMapTo<GamePlayer>
{
    public long GameId { get; set; }

    public long PlayerId { get; set; }

    public int? StatusId { get; set; }

    public Guid UserId { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<CreateGamePlayerDto, GamePlayer>()
        .ForMember(dest => dest.StatusId, opt => opt.NullSubstitute(GamePlayerStatusEnum.OutOfGame));
}