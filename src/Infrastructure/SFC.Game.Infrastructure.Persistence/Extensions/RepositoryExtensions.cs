using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SFC.Game.Application.Common.Settings;
using SFC.Game.Application.Interfaces.Persistence.Repository.Common;
using SFC.Game.Application.Interfaces.Persistence.Repository.Data;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Data;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.General;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Player;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;
using SFC.Game.Application.Interfaces.Persistence.Repository.Identity;
using SFC.Game.Application.Interfaces.Persistence.Repository.Invite.Data;
using SFC.Game.Application.Interfaces.Persistence.Repository.Metadata;
using SFC.Game.Application.Interfaces.Persistence.Repository.Player;
using SFC.Game.Application.Interfaces.Persistence.Repository.Request.Data;
using SFC.Game.Application.Interfaces.Persistence.Repository.Team.Data;
using SFC.Game.Application.Interfaces.Persistence.Repository.Team.General;
using SFC.Game.Application.Interfaces.Persistence.Repository.Team.Player;
using SFC.Game.Infrastructure.Persistence.Repositories.Common;
using SFC.Game.Infrastructure.Persistence.Repositories.Data;
using SFC.Game.Infrastructure.Persistence.Repositories.Data.Cache;
using SFC.Game.Infrastructure.Persistence.Repositories.Game.Data;
using SFC.Game.Infrastructure.Persistence.Repositories.Game.Data.Cache;
using SFC.Game.Infrastructure.Persistence.Repositories.Game.General;
using SFC.Game.Infrastructure.Persistence.Repositories.Game.Player;
using SFC.Game.Infrastructure.Persistence.Repositories.Game.Team.General;
using SFC.Game.Infrastructure.Persistence.Repositories.Game.Team.Player;
using SFC.Game.Infrastructure.Persistence.Repositories.Identity;
using SFC.Game.Infrastructure.Persistence.Repositories.Invite.Data;
using SFC.Game.Infrastructure.Persistence.Repositories.Invite.Data.Cache;
using SFC.Game.Infrastructure.Persistence.Repositories.Metadata;
using SFC.Game.Infrastructure.Persistence.Repositories.Player;
using SFC.Game.Infrastructure.Persistence.Repositories.Request.Data;
using SFC.Game.Infrastructure.Persistence.Repositories.Request.Data.Cache;
using SFC.Game.Infrastructure.Persistence.Repositories.Team.Data;
using SFC.Game.Infrastructure.Persistence.Repositories.Team.Data.Cache;
using SFC.Game.Infrastructure.Persistence.Repositories.Team.General;
using SFC.Game.Infrastructure.Persistence.Repositories.Team.Player;

namespace SFC.Game.Infrastructure.Persistence.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddScoped(typeof(IRepository<,,>), typeof(Repository<,,>));
        services.AddScoped<IMetadataRepository, MetadataRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<ITeamPlayerRepository, TeamPlayerRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IGameTeamRepository, GameTeamRepository>();
        services.AddScoped<IGamePlayerRepository, GamePlayerRepository>();
        services.AddScoped<IGameTeamPlayerRepository, GameTeamPlayerRepository>();

        CacheSettings? cacheSettings = configuration
           .GetSection(CacheSettings.SectionKey)
           .Get<CacheSettings>();

        if (cacheSettings?.Enabled ?? false)
        {
            // data
            services.AddScoped<FootballPositionRepository>();
            services.AddScoped<IFootballPositionRepository, FootballPositionCacheRepository>();
            services.AddScoped<GameStyleRepository>();
            services.AddScoped<IGameStyleRepository, GameStyleCacheRepository>();
            services.AddScoped<StatCategoryRepository>();
            services.AddScoped<IStatCategoryRepository, StatCategoryCacheRepository>();
            services.AddScoped<StatSkillRepository>();
            services.AddScoped<IStatSkillRepository, StatSkillCacheRepository>();
            services.AddScoped<StatTypeRepository>();
            services.AddScoped<IStatTypeRepository, StatTypeCacheRepository>();
            services.AddScoped<WorkingFootRepository>();
            services.AddScoped<IWorkingFootRepository, WorkingFootCacheRepository>();
            services.AddScoped<ShirtRepository>();
            services.AddScoped<IShirtRepository, ShirtCacheRepository>();
            // team
            services.AddScoped<TeamPlayerStatusRepository>();
            services.AddScoped<ITeamPlayerStatusRepository, TeamPlayerStatusCacheRepository>();
            // invite
            services.AddScoped<InviteStatusRepository>();
            services.AddScoped<IInviteStatusRepository, InviteStatusCacheRepository>();
            // request
            services.AddScoped<RequestStatusRepository>();
            services.AddScoped<IRequestStatusRepository, RequestStatusCacheRepository>();
            // game
            services.AddScoped<GameStatusRepository>();
            services.AddScoped<IGameStatusRepository, GameStatusCacheRepository>();
            services.AddScoped<GameTeamStatusRepository>();
            services.AddScoped<IGameTeamStatusRepository, GameTeamStatusCacheRepository>();
            services.AddScoped<GamePlayerStatusRepository>();
            services.AddScoped<IGamePlayerStatusRepository, GamePlayerStatusCacheRepository>();
            services.AddScoped<GameTeamIndexRepository>();
            services.AddScoped<IGameTeamIndexRepository, GameTeamIndexCacheRepository>();
        }
        else
        {
            // data
            services.AddScoped<IFootballPositionRepository, FootballPositionRepository>();
            services.AddScoped<IGameStyleRepository, GameStyleRepository>();
            services.AddScoped<IStatCategoryRepository, StatCategoryRepository>();
            services.AddScoped<IStatSkillRepository, StatSkillRepository>();
            services.AddScoped<IStatTypeRepository, StatTypeRepository>();
            services.AddScoped<IWorkingFootRepository, WorkingFootRepository>();
            services.AddScoped<IShirtRepository, ShirtRepository>();
            // team
            services.AddScoped<ITeamPlayerStatusRepository, TeamPlayerStatusRepository>();
            // invite
            services.AddScoped<IInviteStatusRepository, InviteStatusRepository>();
            // request
            services.AddScoped<IRequestStatusRepository, RequestStatusRepository>();
            // game
            services.AddScoped<IGameStatusRepository, GameStatusRepository>();
            services.AddScoped<IGameTeamStatusRepository, GameTeamStatusRepository>();
            services.AddScoped<IGamePlayerStatusRepository, GamePlayerStatusRepository>();
            services.AddScoped<IGameTeamIndexRepository, GameTeamIndexRepository>();
        }

        return services;
    }
}