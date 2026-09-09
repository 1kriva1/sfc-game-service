namespace SFC.Game.Application.Features.Game.General.Queries.Find.Dto.Filters;
public class GetGamesProfileFilterDto
{
    public GetGamesGeneralProfileFilterDto? General { get; set; }

    public GetGamesFinancialProfileFilterDto? Financial { get; set; }

    public GetGamesInventaryProfileFilterDto? Inventary { get; set; }
}