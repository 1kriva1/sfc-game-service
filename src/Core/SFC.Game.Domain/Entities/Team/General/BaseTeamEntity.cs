using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Entities.Team.General;
public abstract class BaseTeamEntity : BaseEntity<long>
{
    public TeamEntity Team { get; set; } = null!;
}