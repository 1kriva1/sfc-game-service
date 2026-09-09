using AutoMapper;

using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.Player.Common;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Player.Queries.Find;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.Find;

/// <summary>
/// **Get** game players response.
/// </summary>
public class GetGamePlayersResponse : BaseListResponse<GamePlayerModel>, IMapFrom<GetGamePlayersViewModel>
{
    public void Mapping(Profile profile) => profile.CreateMap<GetGamePlayersViewModel, GetGamePlayersResponse>()
                                                   .IgnoreAllNonExisting();
}