using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Data.Queries.Common.Dto;

namespace SFC.Game.Api.Infrastructure.Models.Game.Data.Common;

/// <summary>
/// Data value.
/// </summary>
public class DataValueModel : IMapFrom<DataValueDto>
{
    /// <summary>
    /// Unique identificator of data type.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of data type.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Describe data type.
    /// </summary>
    public required string Title { get; set; }
}