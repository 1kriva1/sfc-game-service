using SFC.Game.Api.Services;

namespace SFC.Game.Api.Infrastructure.Extensions;

public static class GrpcExtensions
{
    public static WebApplication UseGrpc(this WebApplication app)
    {
        app.MapGrpcService<GameDataService>();
        app.MapGrpcService<GameService>();
        app.MapGrpcService<GameTeamService>();
        app.MapGrpcService<GamePlayerService>();
        app.MapGrpcService<GameTeamPlayerService>();

        return app;
    }
}