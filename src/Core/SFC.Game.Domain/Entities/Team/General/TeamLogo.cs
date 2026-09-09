namespace SFC.Game.Domain.Entities.Team.General;
public class TeamLogo : BaseTeamEntity
{
#pragma warning disable CA1819 // Properties should not return arrays
    public required byte[] Source { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays

    public required string Name { get; set; }

    public PhotoExtensionEnum Extension { get; set; }

    public int Size { get; set; }
}