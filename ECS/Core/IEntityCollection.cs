namespace ECS;

public interface IEntityCollection : IDisposable
{
    void ActuallyDestroyEntities();
}