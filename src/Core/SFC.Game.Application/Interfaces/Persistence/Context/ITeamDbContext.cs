using SFC.Game.Domain.Entities.Team.Data;
using SFC.Game.Domain.Entities.Team.General;
using SFC.Game.Domain.Entities.Team.Player;

namespace SFC.Game.Application.Interfaces.Persistence.Context;
public interface ITeamDbContext : IDbContext
{
    #region General

    IQueryable<TeamEntity> Teams { get; }

    IQueryable<TeamGeneralProfile> GeneralProfiles { get; }

    IQueryable<TeamFinancialProfile> FinancialProfiles { get; }

    IQueryable<TeamAvailability> Availability { get; }

    IQueryable<TeamTag> Tags { get; }

    IQueryable<TeamShirt> Shirts { get; }

    IQueryable<TeamLogo> Logos { get; }

    IQueryable<TeamPlayer> TeamPlayers { get; }

    #endregion General

    #region Data

    IQueryable<TeamPlayerStatus> TeamPlayerStatuses { get; }

    #endregion Data
}