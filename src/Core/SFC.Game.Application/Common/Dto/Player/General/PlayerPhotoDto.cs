using AutoMapper;

using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Converters.File;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Common.Dto.Common;
using SFC.Game.Domain.Entities.Player;

namespace SFC.Game.Application.Common.Dto.Player.General;
public class PlayerPhotoDto : FileDto, IMapFromReverse<PlayerPhoto>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlayerPhoto, PlayerPhotoDto>();

        profile.CreateMap<string?, PlayerPhotoDto?>()
               .ConvertUsing<Base64StringToFileTypeConverter<PlayerPhotoDto>>();

        profile.CreateMap<PlayerPhotoDto?, string?>()
               .ConvertUsing<FileToBase64StringTypeConverter<PlayerPhotoDto>>();

        profile.CreateMap<PlayerPhotoDto, PlayerPhoto>()
               .IgnoreAllNonExisting();
    }
}