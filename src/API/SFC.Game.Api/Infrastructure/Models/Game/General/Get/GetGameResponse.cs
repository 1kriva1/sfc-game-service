using AutoMapper;

using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.General.Common;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Queries.Get;

#pragma warning disable CA1716
namespace SFC.Game.Api.Infrastructure.Models.Game.General.Get;
#pragma warning restore CA1716

/// <summary>
/// **Get** game response.
/// </summary>
public class GetGameResponse :
    BaseErrorResponse, IMapFrom<GetGameViewModel>
{
    /// <summary>
    /// Game model.
    /// </summary>
    public GameModel Game { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<GetGameViewModel, GetGameResponse>()
                                                   .IgnoreAllNonExisting();
}