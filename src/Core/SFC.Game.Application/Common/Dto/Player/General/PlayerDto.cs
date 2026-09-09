using AutoMapper;

using SFC.Game.Application.Common.Dto.Common;
using SFC.Game.Application.Common.Mappings.Interfaces;

namespace SFC.Game.Application.Common.Dto.Player.General;

public class PlayerDto : AuditableDto, IMapFromReverse<PlayerEntity>
{
    public long Id { get; set; }

    public Guid UserId { get; set; }

    public PlayerProfileDto? Profile { get; set; }

    public PlayerStatsDto? Stats { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlayerDto, PlayerEntity>()
                .ForMember(d => d.GeneralProfile, o =>
                {
                    o.PreCondition(s => s.Profile is not null);
                    o.MapFrom(s => s.Profile!.General);
                })
                .ForMember(d => d.FootballProfile, o =>
                {
                    o.PreCondition(s => s.Profile is not null);
                    o.MapFrom(s => s.Profile!.Football);
                })
                .ForMember(d => d.Availability, o =>
                {
                    o.PreCondition(s => s.Profile is not null);
                    o.MapFrom(s => s.Profile!.General.Availability);
                })
                .ForMember(d => d.Photo, o =>
                {
                    o.PreCondition(s => s.Profile is not null);
                    o.MapFrom(s => s.Profile!.General.Photo);
                })
                .ForMember(d => d.Tags, o =>
                {
                    o.PreCondition(s => s.Profile is not null);
                    o.MapFrom(s => s.Profile!.General.Tags);
                })
                .ForMember(d => d.Points, o =>
                {
                    o.PreCondition(s => s.Stats is not null);
                    o.MapFrom(s => s.Stats!.Points);
                })
                .ForMember(d => d.Stats, o =>
                {
                    o.PreCondition(s => s.Stats is not null);
                    o.MapFrom(s => s.Stats!.Values);
                })
                .ForMember(p => p.DomainEvents, d => d.Ignore());

        profile.CreateMap<PlayerEntity, PlayerDto>()
                .ForMember(p => p.Profile, d =>
                {
                    d.Condition(s => s.GeneralProfile != null || s.FootballProfile != null);
                    d.MapFrom((src, dst, destMember, context) => new PlayerProfileDto
                    {
                        General = context.Mapper.Map<PlayerGeneralProfileDto>(src),
                        Football = context.Mapper.Map<PlayerFootballProfileDto>(src.FootballProfile)
                    });
                })
                .ForMember(p => p.Stats, d =>
                {
                    d.Condition(s => s.Stats.Count != 0);
                    d.MapFrom((src, dst, destMember, context) => new PlayerStatsDto
                    {
                        Points = context.Mapper.Map<PlayerStatPointsDto>(src.Points),
                        Values = context.Mapper.Map<IEnumerable<PlayerStatValueDto>>(src.Stats)
                    });
                });
    }
}