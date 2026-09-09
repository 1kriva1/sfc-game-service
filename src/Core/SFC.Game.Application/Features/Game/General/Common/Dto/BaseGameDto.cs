using AutoMapper;

using SFC.Game.Application.Common.Dto.Common;
using SFC.Game.Application.Common.Mappings.Interfaces;

namespace SFC.Game.Application.Features.Game.General.Common.Dto;

public class BaseGameDto : AuditableDto, IMapToReverse<GameEntity>
{
    public GameProfileDto? Profile { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BaseGameDto, GameEntity>()
               .ForMember(d => d.GeneralProfile, o =>
               {
                   o.PreCondition(s => s.Profile is not null);
                   o.MapFrom(s => s.Profile!.General);
               })
               .ForMember(d => d.FinancialProfile, o =>
               {
                   o.PreCondition(s => s.Profile is not null);
                   o.MapFrom(s => s.Profile!.Financial);
               })
               .ForMember(d => d.InventaryProfile, o =>
               {
                   o.PreCondition(s => s.Profile is not null);
                   o.MapFrom(s => s.Profile!.Inventary);
               })
               .ForMember(d => d.Availability, o =>
               {
                   o.PreCondition(s => s.Profile is not null);
                   o.MapFrom(s => s.Profile!.General.Availability);
               })
               .ForMember(d => d.Tags, o =>
               {
                   o.PreCondition(s => s.Profile is not null);
                   o.MapFrom(s => s.Profile!.General.Tags);
               })
               .ForMember(d => d.CreatedDate, o => o.Ignore())
               .ForMember(d => d.CreatedBy, o => o.Ignore())
               .ForMember(d => d.LastModifiedDate, o => o.Ignore())
               .ForMember(d => d.LastModifiedBy, o => o.Ignore())
               .ReverseMap();
    }
}