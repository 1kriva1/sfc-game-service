using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Common.Dto;

namespace SFC.Game.Application.Features.Game.General.Commands.Create;
public class CreateGameDto : BaseGameDto, IMapTo<GameEntity>
{
    public new void Mapping(Profile profile)
    {
        profile.CreateMap<CreateGameDto, GameEntity>()
               .IncludeBase<BaseGameDto, GameEntity>();
    }
}