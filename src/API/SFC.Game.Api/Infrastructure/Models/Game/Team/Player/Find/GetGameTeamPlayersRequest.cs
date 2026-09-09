using AutoMapper;

using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Find.Filters;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Queries.Find;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Find;

/// <summary>
/// **Get** game team players request.
/// </summary>
public class GetGameTeamPlayersRequest : BasePaginationRequest<GetGameTeamPlayersFilterModel>, IMapTo<GetGameTeamPlayersQuery>
{
    public void Mapping(Profile profile) => profile.CreateMap<GetGameTeamPlayersRequest, GetGameTeamPlayersQuery>()
                                                   .IgnoreAllNonExisting();
}