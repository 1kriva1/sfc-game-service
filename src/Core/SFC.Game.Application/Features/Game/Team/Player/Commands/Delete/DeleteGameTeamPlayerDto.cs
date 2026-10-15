namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Delete;
public class DeleteGameTeamPlayerDto
{
    public long GameId { get; set; }

    public long TeamId { get; set; }

    public long PlayerId { get; set; }
}