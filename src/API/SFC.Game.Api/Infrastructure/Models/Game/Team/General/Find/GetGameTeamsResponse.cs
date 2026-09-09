using AutoMapper;

using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.Team.General.Common;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.General.Queries.Find;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.General.Find;

/// <summary>
/// **Get** game teams response.
/// </summary>
public class GetGameTeamsResponse : BaseListResponse<GameTeamModel>, IMapFrom<GetGameTeamsViewModel>
{
    public void Mapping(Profile profile) => profile.CreateMap<GetGameTeamsViewModel, GetGameTeamsResponse>()
                                                   .IgnoreAllNonExisting();
}