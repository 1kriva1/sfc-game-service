using SFC.Game.Api.Infrastructure.Models.Game.General.Common;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Commands.Create;

namespace SFC.Game.Api.Infrastructure.Models.Game.General.Create;

/// <summary>
/// **Create** game model.
/// </summary>
public class CreateGameModel : BaseGameModel, IMapTo<CreateGameDto> { }