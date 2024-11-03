namespace ECS;

public sealed class KeyEntityListLink<TKey, TEntity> : IEntityLink<TEntity> 
    where TEntity : Entity where TKey : notnull
{
    readonly Dictionary<TKey, HashSet<TEntity>> _entitiesByKey = new();
    readonly Dictionary<TEntity, TKey> _keyByEntity = new();

    public void Add(TKey key, TEntity entity)
    {
        bool listFound = _entitiesByKey.TryGetValue(key, out HashSet<TEntity>? entities);

        if (!listFound)
        {
            entities = new HashSet<TEntity>();
            _entitiesByKey.Add(key, entities);
        }
        
        entities!.Add(entity);
        _keyByEntity.Add(entity, key);
    }

    public IReadOnlyCollection<TEntity> Get(TKey key)
    {
        bool listFound = _entitiesByKey.TryGetValue(key, out HashSet<TEntity>? entities);

        if (!listFound)
        {
            return Array.Empty<TEntity>();
        }

        return entities!;
    }
    
    public bool TryGet(TEntity entity, out TKey? key)
    {
        return _keyByEntity.TryGetValue(entity, out key);
    }

    public void Remove(TEntity entity)
    {
        bool keyFound = _keyByEntity.TryGetValue(entity, out TKey? key);

        if (!keyFound)
        {
            return;
        }

        _keyByEntity.Remove(entity);
        
        bool listFound = _entitiesByKey.TryGetValue(key!, out HashSet<TEntity>? entities);

        if (!listFound)
        {
            return;
        }
        
        entities!.Remove(entity);
    }
}