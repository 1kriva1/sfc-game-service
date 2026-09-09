using AutoMapper;

using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Commands.Update;

namespace SFC.Game.Api.Infrastructure.Models.Game.General.Update;

/// <summary>
/// **Update** game request.
/// </summary>
public class UpdateGameRequest : IMapTo<UpdateGameCommand>
{
    /// <summary>
    /// Game model.
    /// </summary>
    public UpdateGameModel Game { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<UpdateGameRequest, UpdateGameCommand>()
                                                   .IgnoreAllNonExisting();
}