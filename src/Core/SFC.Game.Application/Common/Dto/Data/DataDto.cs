namespace SFC.Game.Application.Common.Dto.Data;

/// <summary>
/// Parent class for all data entities.
/// </summary>
public class DataDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
}