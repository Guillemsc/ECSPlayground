namespace ECS;

public sealed class KeyEntityLink<TKey, TEntity> : IEntityLink<TEntity> 
    where TEntity : Entity where TKey : notnull
{
    readonly Dictionary<TKey, TEntity> _entityByKey = new();
    readonly Dictionary<TEntity, TKey> _keyByEntity = new();

    public void Add(TKey key, TEntity entity)
    {
        _entityByKey.Add(key, entity);
        _keyByEntity.Add(entity, key);
    }

    public bool TryGet(TKey key, out TEntity? entity)
    {
        return _entityByKey.TryGetValue(key, out entity);
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

        _entityByKey.Remove(key!);
        _keyByEntity.Remove(entity);
    }
}