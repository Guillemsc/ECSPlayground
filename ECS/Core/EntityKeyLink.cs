namespace ECS;

public sealed class EntityKeyLink<TEntity, TKey> : IEntityLink<TEntity> 
    where TEntity : Entity where TKey : notnull
{
    readonly Dictionary<TEntity, TKey> _keyByEntity = new();

    public void Add(TEntity entity, TKey key)
    {
        _keyByEntity.Add(entity, key);
    }

    public bool TryGet(TEntity entity, out TKey? key)
    {
        return _keyByEntity.TryGetValue(entity, out key);
    }

    public void Remove(TEntity entity)
    {
        _keyByEntity.Remove(entity);
    }
}