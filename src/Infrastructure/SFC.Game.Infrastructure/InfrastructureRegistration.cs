using System.Reflection;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using SFC.Game.Application.Interfaces.Common;
using SFC.Game.Application.Interfaces.Game.Data;
using SFC.Game.Application.Interfaces.Game.General;
using SFC.Game.Application.Interfaces.Game.Player;
using SFC.Game.Application.Interfaces.Game.Team.General;
using SFC.Game.Application.Interfaces.Game.Team.Player;
using SFC.Game.Application.Interfaces.Identity;
using SFC.Game.Application.Interfaces.Metadata;
using SFC.Game.Application.Interfaces.Player;
using SFC.Game.Application.Interfaces.Reference;
using SFC.Game.Application.Interfaces.Team.General;
using SFC.Game.Application.Interfaces.Team.Player;
using SFC.Game.Infrastructure.Authorization.OwnGame;
using SFC.Game.Infrastructure.Authorization.OwnPlayer;
using SFC.Game.Infrastructure.Authorization.OwnTeam;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Extensions.Grpc;
using SFC.Game.Infrastructure.Services.Common;
using SFC.Game.Infrastructure.Services.Game.Data;
using SFC.Game.Infrastructure.Services.Game.General;
using SFC.Game.Infrastructure.Services.Game.Player;
using SFC.Game.Infrastructure.Services.Game.Team.General;
using SFC.Game.Infrastructure.Services.Game.Team.Player;
using SFC.Game.Infrastructure.Services.Hosted;
using SFC.Game.Infrastructure.Services.Identity;
using SFC.Game.Infrastructure.Services.Metadata;
using SFC.Game.Infrastructure.Services.Player;
using SFC.Game.Infrastructure.Services.Reference;
using SFC.Game.Infrastructure.Services.Team.General;
using SFC.Game.Infrastructure.Services.Team.Player;
using SFC.Scheme.Infrastructure.Extensions;

namespace SFC.Game.Infrastructure;
public static class InfrastructureRegistration
{
    public static void AddInfrastructureServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddAutoMapper(config => { }, Assembly.GetExecutingAssembly());

        builder.Services.AddHangfire(builder.Configuration);

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAccessTokenManagement();

        builder.Services.AddRedis(builder.Configuration);

        builder.AddMassTransit();

        builder.Services.AddCache(builder.Configuration);

        builder.Services.AddGrpc(builder.Configuration, builder.Environment);

        builder.Services.AddSingleton<IUriService>(o =>
        {
            IHttpContextAccessor accessor = o.GetRequiredService<IHttpContextAccessor>();
            HttpRequest request = accessor.HttpContext!.Request;
            return new UriService(string.Concat(request.Scheme, "://", request.Host.ToUriComponent()));
        });

        // custom services
        builder.Services.AddTransient<IMetadataService, MetadataService>();
        builder.Services.AddTransient<IDateTimeService, DateTimeService>();
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<IUserSeedService, UserSeedService>();
        builder.Services.AddTransient<IGameDataService, GameDataService>();
        builder.Services.AddTransient<IPlayerSeedService, PlayerSeedService>();
        builder.Services.AddTransient<ITeamSeedService, TeamSeedService>();
        builder.Services.AddTransient<ITeamPlayerSeedService, TeamPlayerSeedService>();
        builder.Services.AddTransient<IGameService, GameService>();
        builder.Services.AddTransient<IGameSeedService, GameSeedService>();
        builder.Services.AddTransient<IGameTeamService, GameTeamService>();
        builder.Services.AddTransient<IGameTeamSeedService, GameTeamSeedService>();
        builder.Services.AddTransient<IGamePlayerService, GamePlayerService>();
        builder.Services.AddTransient<IGamePlayerSeedService, GamePlayerSeedService>();
        builder.Services.AddTransient<IGameTeamPlayerService, GameTeamPlayerService>();
        builder.Services.AddTransient<IGameTeamPlayerSeedService, GameTeamPlayerSeedService>();

        // grpc
        builder.Services.AddTransient<IIdentityService, IdentityService>();
        builder.Services.AddTransient<IPlayerService, PlayerService>();
        builder.Services.AddTransient<ITeamService, TeamService>();

        // references
        builder.Services.AddScoped<IIdentityReference, IdentityReference>();
        builder.Services.AddScoped<IPlayerReference, PlayerReference>();
        builder.Services.AddScoped<ITeamReference, TeamReference>();

        // hosted services
        builder.Services.AddHostedService<DatabaseResetHostedService>();
        builder.Services.AddHostedService<DataInitializationHostedService>();
        builder.Services.AddHostedService<JobsInitializationHostedService>();

        // authorization
        builder.Services.AddScoped<IAuthorizationHandler, OwnGameHandler>();
        builder.Services.AddScoped<IAuthorizationHandler, OwnPlayerHandler>();
        builder.Services.AddScoped<IAuthorizationHandler, OwnTeamHandler>();
    }
}