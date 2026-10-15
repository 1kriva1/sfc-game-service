using AutoMapper;

using SFC.Game.Application.Common.Dto.Common;
using SFC.Game.Application.Common.Dto.Team.Player;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Team.General;

namespace SFC.Game.Application.Common.Dto.Team.General;

public class TeamDto : AuditableDto, IMapFromReverse<TeamEntity>
{
    public long Id { get; set; }

    public Guid UserId { get; set; }

    public int StatusId { get; set; }

    public TeamProfileDto? Profile { get; set; }

    public IEnumerable<TeamPlayerDto>? Players { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TeamDto, TeamEntity>()
                .ForMember(p => p.GeneralProfile, d => d.MapFrom(z => z.Profile!.General))
                .ForMember(p => p.FinancialProfile, d => d.MapFrom(z => z.Profile!.Financial))
                .ForMember(p => p.InventaryProfile, d => d.MapFrom(z => z.Profile!.Inventary))
                .ForMember(p => p.Availability, d => d.MapFrom(z => z.Profile!.General.Availability))
                .ForMember(p => p.Logo, d => d.MapFrom(z => z.Profile!.General.Logo))
                .ForMember(p => p.Tags, d => d.MapFrom(z => z.Profile!.General.Tags))
                .ForMember(p => p.Shirts, d => d.MapFrom(z => z.Profile!.Inventary.Shirts))
                .ForMember(p => p.DomainEvents, d => d.Ignore());

        profile.CreateMap<TeamEntity, TeamDto>()
                .ForMember(p => p.Profile, d =>
                {
                    d.Condition(s => s.GeneralProfile != null || s.FinancialProfile != null || s.InventaryProfile != null);
                    d.MapFrom((src, dst, destMember, context) => new TeamProfileDto
                    {
                        General = context.Mapper.Map<TeamGeneralProfileDto>(src),
                        Financial = context.Mapper.Map<TeamFinancialProfileDto>(src.FinancialProfile),
                        Inventary = context.Mapper.Map<TeamInventaryProfileDto>(src)
                    });
                });
    }
}