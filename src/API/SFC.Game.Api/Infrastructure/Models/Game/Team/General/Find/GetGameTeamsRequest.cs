using AutoMapper;

using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.Team.General.Find.Filters;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.General.Queries.Find;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.General.Find;

/// <summary>
/// **Get** game teams request.
/// </summary>
public class GetGameTeamsRequest : BasePaginationRequest<GetGameTeamsFilterModel>, IMapTo<GetGameTeamsQuery>
{
    public void Mapping(Profile profile) => profile.CreateMap<GetGameTeamsRequest, GetGameTeamsQuery>()
                                                   .IgnoreAllNonExisting();
}