namespace ECS;

public sealed class EntityPool<TEntity> where TEntity : Entity
{
    readonly List<TEntity> _avaliable = new();
    readonly HashSet<TEntity> _used = new();

    public TEntity Get(int id)
    {
        TEntity entity;
        
        if (_avaliable.Count == 0)
        {
            entity = (TEntity)Activator.CreateInstance(typeof(TEntity), id)!;
        }
        else
        {
            entity = _avaliable[0];
            _avaliable.RemoveAt(0);
            entity.Reset(id);
        }

        _used.Add(entity);

        return entity;
    }

    public void Return(TEntity entity)
    {
        bool wasUsed = _used.Remove(entity);

        if (!wasUsed)
        {
            return;
        }
        
        _avaliable.Add(entity);
    }
}