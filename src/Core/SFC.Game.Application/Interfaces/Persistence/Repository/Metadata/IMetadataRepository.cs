using SFC.Game.Application.Interfaces.Persistence.Context;
using SFC.Game.Application.Interfaces.Persistence.Repository.Common;

namespace SFC.Game.Application.Interfaces.Persistence.Repository.Metadata;

/// <summary>
/// Repository for metadata of the service.
/// </summary>
public interface IMetadataRepository : IRepository<MetadataEntity, IMetadataDbContext, int>
{
    /// <summary>
    /// Get metadata entity by service and type values.
    /// </summary>
    /// <param name="service">Metadata service filter value.</param>
    /// <param name="type">Metadata type filter value.</param>
    /// <returns>Metadata entity or null.</returns>
    Task<MetadataEntity?> GetByIdAsync(MetadataServiceEnum service, MetadataDomainEnum domain, MetadataTypeEnum type);
}