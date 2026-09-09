using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;

namespace SFC.Game.Application.Common.Dto.Team.General;
public class TeamProfileDto : IMapFrom<TeamEntity>
{
    public required TeamGeneralProfileDto General { get; set; }

    public required TeamFinancialProfileDto Financial { get; set; }

    public required TeamInventaryProfileDto Inventary { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TeamEntity, TeamProfileDto>()
               .ForMember(p => p.General, d => d.MapFrom(z => z))
               .ForMember(p => p.Financial, d => d.MapFrom(z => z.FinancialProfile))
               .ForMember(p => p.Inventary, d => d.MapFrom(z => z));
    }
}