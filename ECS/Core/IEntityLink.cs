namespace ECS;

public interface IEntityLink<in TEntity> where TEntity : Entity
{
    void Remove(TEntity entity);
}