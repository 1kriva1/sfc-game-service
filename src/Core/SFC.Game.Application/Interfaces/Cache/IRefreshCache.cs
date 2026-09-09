namespace SFC.Game.Application.Interfaces.Cache;

// Refresh cache contract.
public interface IRefreshCache
{
    Task RefreshAsync(CancellationToken token = default);
}