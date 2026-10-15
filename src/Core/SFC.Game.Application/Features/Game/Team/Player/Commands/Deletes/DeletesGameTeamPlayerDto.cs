namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Deletes;
public class DeletesGameTeamPlayerDto
{
    public long GameId { get; set; }

    public long TeamId { get; set; }

    public long PlayerId { get; set; }
}