namespace ECS;

public sealed class EntityCollection<TEntity> : IEntityCollection where TEntity : Entity
{
    public IEnumerable<TEntity> Entities => _entitiesById.Values.Where(e => e.IsValid);

    readonly EntityPool<TEntity> _pool = new();
    readonly Dictionary<int, TEntity> _entitiesById = new();
    readonly Dictionary<int, TEntity> _entitiesByIdToDestroy = new();
    readonly List<IEntityLink<TEntity>> _entityLinks = new();

    int _lastId;

    public void Dispose()
    {
        foreach (TEntity entity in _entitiesById.Values)
        {
            DestroyEntity(entity);
        }
        
        ActuallyDestroyEntities();
    }
    
    public TEntity CreateEntity()
    {
        TEntity entity = _pool.Get(_lastId);

        ++_lastId;
        
        _entitiesById.Add(entity.Id, entity);

        return entity;
    }

    public void DestroyEntity(TEntity entity)
    {
        if (!entity.IsValid)
        {
            return;
        }
        
        _entitiesByIdToDestroy.Add(entity.Id, entity);
        entity.Invalidate();
    }

    public void ActuallyDestroyEntities()
    {
        foreach (KeyValuePair<int, TEntity> entry in _entitiesByIdToDestroy)
        {
            foreach (IEntityLink<TEntity> entityLink in _entityLinks)
            {
                entityLink.Remove(entry.Value);
            }
            
            _entitiesById.Remove(entry.Key);
            _pool.Return(entry.Value);
        }
        
        _entitiesByIdToDestroy.Clear();
    }
    
    public void AddEntityLink(IEntityLink<TEntity> link)
    {
        _entityLinks.Add(link);
    }
}