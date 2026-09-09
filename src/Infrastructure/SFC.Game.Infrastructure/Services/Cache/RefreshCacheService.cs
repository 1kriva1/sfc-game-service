using SFC.Game.Application.Interfaces.Cache;
using SFC.Game.Application.Interfaces.Game.Data;
using SFC.Game.Application.Interfaces.Game.Data.Models;

namespace SFC.Game.Infrastructure.Services.Cache;
public class RefreshCacheService(ICache cache, IGameDataService gameDataService) : IRefreshCache
{
    private readonly ICache _cache = cache;
    private readonly IGameDataService _gameDataService = gameDataService;

    public async Task RefreshAsync(CancellationToken token = default)
    {
        GetAllGameDataModel model = await _gameDataService.GetAllGameDataAsync().ConfigureAwait(false);

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        RefreshAsync(model.GameStatuses, token);

        RefreshAsync(model.GameTeamStatuses, token);

        RefreshAsync(model.GamePlayerStatuses, token);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
    }

    private Task RefreshAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _cache.DeleteAsync($"{typeof(T).Name}", cancellationToken);
        return _cache.SetAsync($"{typeof(T).Name}", entities, null, cancellationToken);
    }
}