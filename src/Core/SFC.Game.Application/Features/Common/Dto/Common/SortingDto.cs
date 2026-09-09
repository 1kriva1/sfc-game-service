using SFC.Game.Application.Common.Enums;

namespace SFC.Game.Application.Features.Common.Dto.Common;

/// <summary>
/// Sorting DTO.
/// </summary>
public class SortingDto
{
    public required string Name { get; set; }

    public SortingDirection Direction { get; set; }
}