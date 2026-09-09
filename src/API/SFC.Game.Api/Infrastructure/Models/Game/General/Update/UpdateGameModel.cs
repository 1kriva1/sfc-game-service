using SFC.Game.Api.Infrastructure.Models.Game.General.Common;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Commands.Update;

namespace SFC.Game.Api.Infrastructure.Models.Game.General.Update;

/// <summary>
/// **Update** game model.
/// </summary>
public class UpdateGameModel : BaseGameModel, IMapTo<UpdateGameDto> { }