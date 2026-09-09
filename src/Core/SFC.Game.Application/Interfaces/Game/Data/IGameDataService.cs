using SFC.Game.Application.Interfaces.Game.Data.Models;

namespace SFC.Game.Application.Interfaces.Game.Data;
public interface IGameDataService
{
    Task<GetAllGameDataModel> GetAllGameDataAsync();

    Task<GetInviteDataModel> GetInviteDataAsync();

    Task<GetRequestDataModel> GetRequestDataAsync();

    Task<GetSchemeDataModel> GetSchemeDataAsync();

    Task PublishDataInitializedEventAsync(CancellationToken cancellationToken);
}