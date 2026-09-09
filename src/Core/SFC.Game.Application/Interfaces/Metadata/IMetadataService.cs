namespace SFC.Game.Application.Interfaces.Metadata;

/// <summary>
/// Service metadata contract.
/// </summary>
public interface IMetadataService
{
    Task CompleteAsync(MetadataServiceEnum service, MetadataDomainEnum domain, MetadataTypeEnum type);

    Task<bool> IsCompletedAsync(MetadataServiceEnum service, MetadataDomainEnum domain, MetadataTypeEnum type);
}