namespace SFC.Game.Application.Interfaces.Identity;

/// <summary>
/// Users seed service (from Identity service).
/// </summary>
public interface IUserSeedService
{
    Task SendRequireUsersSeedAsync(CancellationToken cancellationToken = default);
}