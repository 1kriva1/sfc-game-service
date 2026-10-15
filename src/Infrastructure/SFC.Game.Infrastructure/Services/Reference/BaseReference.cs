using AutoMapper;

using Microsoft.EntityFrameworkCore;

using SFC.Game.Application.Interfaces.Reference;
using SFC.Game.Domain.Common;
using SFC.Game.Infrastructure.Persistence.Extensions;

namespace SFC.Game.Infrastructure.Services.Reference;
public abstract class BaseReference<TEntity, TId, TDto>(IMapper mapper)
    : IReference<TEntity, TId, TDto> where TEntity : BaseEntity<TId>
{
    private readonly IMapper _mapper = mapper;

    protected abstract Task<TEntity?> GetFromLocalAsync(TId id, CancellationToken cancellationToken = default);

    protected abstract Task<TDto?> GetFromReferenceAsync(TId id, CancellationToken cancellationToken = default);

    protected abstract Task<TEntity> AddLocalAsync(TEntity entity, CancellationToken cancellationToken = default);

    protected abstract void DetachLocal(TEntity entity);

    public async Task<TEntity?> GetAsync(TId id, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await GetFromLocalAsync(id, cancellationToken).ConfigureAwait(true);
        if (entity is not null) return entity;

        TDto? reference = await GetFromReferenceAsync(id, cancellationToken).ConfigureAwait(true);
        if (reference is null) return null;

        // check again before direct insert — another request/consumer may have won the race
        entity = await GetFromLocalAsync(id, cancellationToken).ConfigureAwait(true);

        if (entity is not null) return entity;

        entity = _mapper.Map<TEntity>(reference);

        try
        {
            return await AddLocalAsync(entity, cancellationToken).ConfigureAwait(false);
        }
        catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
        {
            DetachLocal(entity);
            return await GetFromLocalAsync(id, cancellationToken).ConfigureAwait(true);
        }
    }
}