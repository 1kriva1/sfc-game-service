namespace SFC.Game.Application.Interfaces.Persistence.Context;

/// <summary>
/// Metadata DB context.
/// </summary>
public interface IMetadataDbContext : IDbContext
{
    IQueryable<MetadataEntity> Metadata { get; }
}