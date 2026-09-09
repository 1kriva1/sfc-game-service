using Microsoft.EntityFrameworkCore;

using SFC.Game.Application.Interfaces.Common;
using SFC.Game.Domain.Entities.Game.Data;
using SFC.Game.Infrastructure.Persistence.Extensions;

namespace SFC.Game.Infrastructure.Persistence.Seeds;
public static class GameDataSeed
{
    public static void SeedGameData(this ModelBuilder builder, IDateTimeService dateTimeService)
    {
        builder.SeedDataEnumValues<GameStatus, GameStatusEnum>(@enum =>
            new GameStatus(@enum).SetCreatedDate(dateTimeService));

        builder.SeedDataEnumValues<GameTeamStatus, GameTeamStatusEnum>(@enum =>
            new GameTeamStatus(@enum).SetCreatedDate(dateTimeService));

        builder.SeedDataEnumValues<GamePlayerStatus, GamePlayerStatusEnum>(@enum =>
            new GamePlayerStatus(@enum).SetCreatedDate(dateTimeService));

        builder.SeedDataEnumValues<GameTeamIndex, GameTeamIndexEnum>(@enum =>
            new GameTeamIndex(@enum).SetCreatedDate(dateTimeService));
    }
}