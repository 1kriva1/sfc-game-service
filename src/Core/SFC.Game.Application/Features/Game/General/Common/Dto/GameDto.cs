using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Player.Common.Dto;
using SFC.Game.Application.Features.Game.Team.General.Common.Dto;

namespace SFC.Game.Application.Features.Game.General.Common.Dto;
public class GameDto : BaseGameDto, IMapFrom<GameEntity>
{
    public long Id { get; set; }

    public Guid UserId { get; set; }

    public int StatusId { get; set; }

    public IEnumerable<GamePlayerDto>? Players { get; set; }

    public IEnumerable<GameTeamDto>? Teams { get; set; }

    public new void Mapping(Profile profile)
    {
        profile.CreateMap<GameEntity, GameDto>()
               .ForMember(p => p.Profile, d => d.MapFrom(s => s.GeneralProfile != null || s.FinancialProfile != null || s.InventaryProfile != null ? s : null));
    }
}