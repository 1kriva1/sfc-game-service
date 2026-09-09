using SFC.Game.Domain.Entities.Game.Data;
using SFC.Game.Domain.Entities.Game.General;
using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Team.Player;

namespace SFC.Game.Application.Interfaces.Persistence.Context;

/// <summary>
/// Core DB context of the service.
/// </summary>
public interface IGameDbContext : IDbContext
{
    #region General

    IQueryable<GameEntity> Games { get; }

    IQueryable<GameGeneralProfile> GeneralProfiles { get; }

    IQueryable<GameFinancialProfile> FinancialProfiles { get; }

    IQueryable<GameInventaryProfile> InventaryProfiles { get; }

    IQueryable<GameAvailability> Availabilities { get; }

    IQueryable<GameTag> Tags { get; }

    #endregion General

    #region Team

    IQueryable<GameTeam> GameTeams { get; }

    IQueryable<GameTeamPlayer> GameTeamPlayers { get; }

    #endregion Team

    #region Player

    IQueryable<GamePlayer> GamePlayers { get; }

    #endregion Player

    #region Data

    IQueryable<GameStatus> GameStatuses { get; }

    IQueryable<GameTeamStatus> GameTeamStatuses { get; }

    IQueryable<GamePlayerStatus> GamePlayerStatuses { get; }

    IQueryable<GameTeamIndex> GameTeamIndexes { get; }

    #endregion Data
}