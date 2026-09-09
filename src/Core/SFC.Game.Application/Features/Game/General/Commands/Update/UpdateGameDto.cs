using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Common.Dto;

namespace SFC.Game.Application.Features.Game.General.Commands.Update;
public class UpdateGameDto : BaseGameDto, IMapTo<GameEntity>
{
    public new void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateGameDto, GameEntity>()
               .IncludeBase<BaseGameDto, GameEntity>();
    }
}