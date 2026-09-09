using System.Reflection;

using SFC.Game.Application.Common.Mappings.Base;
using SFC.Game.Application.Features.Common.Dto.Pagination;
using SFC.Game.Application.Features.Common.Models.Find.Paging;
using SFC.Game.Application.Features.Game.Data.Queries.Common.Dto;
using SFC.Game.Domain.Common;
using SFC.Game.Domain.Entities.Data;
using SFC.Game.Domain.Entities.Game.General;
using SFC.Game.Domain.Entities.Identity;
using SFC.Game.Domain.Entities.Player;
using SFC.Game.Domain.Entities.Team.General;

namespace SFC.Game.Application.Common.Mappings;
public class MappingProfile : BaseMappingProfile
{
    protected override Assembly Assembly => Assembly.GetExecutingAssembly();

    public MappingProfile() : base()
    {
        ApplyCustomMappings();
    }

    private void ApplyCustomMappings()
    {
        #region Simple types

        CreateMap<Guid, User>()
            .ConvertUsing(id => new User { Id = id });

        CreateMap<int, StatType>()
           .ConvertUsing(statType => new StatType { Id = (StatTypeEnum)statType });
        CreateMap<StatType, int>()
            .ConvertUsing(statType => (int)statType.Id);

        CreateMap<DayOfWeek, PlayerAvailableDay>()
            .ConvertUsing(day => new PlayerAvailableDay { Day = day });
        CreateMap<PlayerAvailableDay, DayOfWeek>()
            .ConvertUsing(day => day.Day);

        CreateMap<string, PlayerTag>()
            .ConvertUsing(tag => new PlayerTag { Value = tag });
        CreateMap<PlayerTag, string>()
            .ConvertUsing(tag => tag.Value);

        CreateMap<TeamShirt, int>()
            .ConvertUsing(teamShirt => (int)teamShirt.ShirtId);
        CreateMap<int, TeamShirt>()
            .ConvertUsing(shirtId => new TeamShirt { ShirtId = (ShirtEnum)shirtId });

        CreateMap<string, TeamTag>()
            .ConvertUsing(tag => new TeamTag { Value = tag });
        CreateMap<TeamTag, string>()
            .ConvertUsing(tag => tag.Value);

        CreateMap<string, GameTag>()
            .ConvertUsing(tag => new GameTag { Value = tag });
        CreateMap<GameTag, string>()
            .ConvertUsing(tag => tag.Value);

        #endregion Simple types

        #region Complex types

        #endregion Complex types

        #region Generic types

        CreateMap(typeof(PagedList<>), typeof(PageDto<>))
            .ForMember(nameof(PageDto<object>.Items), d => d.Ignore())
            .ForMember(nameof(PageDto<object>.Metadata), d => d.Ignore());

        CreateMap(typeof(PagedList<>), typeof(PageMetadataDto));

        CreateMap(typeof(EnumDataEntity<>), typeof(DataValueDto));

        #endregion Generic types        
    }
}