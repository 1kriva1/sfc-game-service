using AutoMapper;

using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Commands.Create;

namespace SFC.Game.Api.Infrastructure.Models.Game.General.Create;

/// <summary>
/// **Create** Game request.
/// </summary>
public class CreateGameRequest : IMapTo<CreateGameCommand>
{
    /// <summary>
    /// Game model.
    /// </summary>
    public CreateGameModel Game { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<CreateGameRequest, CreateGameCommand>()
                                                   .IgnoreAllNonExisting();
}