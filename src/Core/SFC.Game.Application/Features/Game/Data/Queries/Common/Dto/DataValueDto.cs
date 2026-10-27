namespace SFC.Game.Application.Features.Game.Data.Queries.Common.Dto;
public class DataValueDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public required string Title { get; set; }
}