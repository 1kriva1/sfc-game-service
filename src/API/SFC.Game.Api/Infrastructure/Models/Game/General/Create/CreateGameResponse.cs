using AutoMapper;

using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.General.Common;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Commands.Create;

namespace SFC.Game.Api.Infrastructure.Models.Game.General.Create;

/// <summary>
/// **Create** game response model.
/// </summary>
public class CreateGameResponse :
    BaseErrorResponse, IMapFrom<CreateGameViewModel>
{
    /// <summary>
    /// Game model.
    /// </summary>
    public GameModel Game { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<CreateGameViewModel, CreateGameResponse>()
                                                   .IgnoreAllNonExisting();
}