using AutoMapper;

using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.General.Common;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Queries.Find;

namespace SFC.Game.Api.Infrastructure.Models.Game.General.Find;

/// <summary>
/// **Get** games response.
/// </summary>
public class GetGamesResponse : BaseListResponse<GameModel>, IMapFrom<GetGamesViewModel>
{
    public void Mapping(Profile profile) => profile.CreateMap<GetGamesViewModel, GetGamesResponse>()
                                                   .IgnoreAllNonExisting();
}