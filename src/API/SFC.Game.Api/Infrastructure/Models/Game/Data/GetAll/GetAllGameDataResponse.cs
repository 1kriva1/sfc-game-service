using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.Data.Common;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Data.Queries.GetAll;

namespace SFC.Game.Api.Infrastructure.Models.Game.Data.GetAll;

/// <summary>
/// Contain all available game **data types**.
/// </summary>
public class GetAllGameDataResponse : BaseErrorResponse, IMapFrom<GetAllGameDataViewModel>
{
    /// <summary>
    /// Game statuses.
    /// </summary>
    public IEnumerable<DataValueModel> GameStatuses { get; init; } = [];

    /// <summary>
    /// Game team statuses.
    /// </summary>
    public IEnumerable<DataValueModel> GameTeamStatuses { get; init; } = [];

    /// <summary>
    /// Game player statuses.
    /// </summary>
    public IEnumerable<DataValueModel> GamePlayerStatuses { get; init; } = [];

    /// <summary>
    /// Game team indexes.
    /// </summary>
    public IEnumerable<DataValueModel> GameTeamIndexes { get; init; } = [];
}