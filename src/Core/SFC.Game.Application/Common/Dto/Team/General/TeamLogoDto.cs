using AutoMapper;

using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Converters.File;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Common.Dto.Common;
using SFC.Game.Domain.Entities.Team.General;

namespace SFC.Game.Application.Common.Dto.Team.General;
public class TeamLogoDto : FileDto, IMapFromReverse<TeamLogo>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TeamLogo, TeamLogoDto>();

        profile.CreateMap<string?, TeamLogoDto?>()
               .ConvertUsing<Base64StringToFileTypeConverter<TeamLogoDto>>();

        profile.CreateMap<TeamLogoDto?, string?>()
               .ConvertUsing<FileToBase64StringTypeConverter<TeamLogoDto>>();

        profile.CreateMap<TeamLogoDto, TeamLogo>()
               .IgnoreAllNonExisting();
    }
}