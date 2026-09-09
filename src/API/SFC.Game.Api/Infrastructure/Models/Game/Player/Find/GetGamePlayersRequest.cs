using AutoMapper;

using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.Team.Find.Filters;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Player.Queries.Find;

namespace SFC.Game.Api.Infrastructure.Models.Game.Player.Find;

/// <summary>
/// **Get** game players request.
/// </summary>
public class GetGamePlayersRequest : BasePaginationRequest<GetGamePlayersFilterModel>, IMapTo<GetGamePlayersQuery>
{
    public void Mapping(Profile profile) => profile.CreateMap<GetGamePlayersRequest, GetGamePlayersQuery>()
                                                   .IgnoreAllNonExisting();
}