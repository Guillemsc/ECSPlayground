namespace ECS;

public abstract class World : IDisposable
{
    readonly List<IEntityCollection> _entityCollections = new();
    readonly List<ITickableSystem> _tickableSystems = new();

    public void Tick()
    {
        foreach (ITickableSystem tickableSystem in _tickableSystems)
        {
            tickableSystem.Tick();
        }
        
        foreach (IEntityCollection entityCollection in _entityCollections)
        {
            entityCollection.ActuallyDestroyEntities();
        }
    }
    
    public void Dispose()
    {
        foreach (IEntityCollection entityCollection in _entityCollections)
        {
            entityCollection.Dispose();
        }
        
        _entityCollections.Clear();
        _tickableSystems.Clear();
    }

    protected void AddEntityCollection(IEntityCollection entityCollection)
    {
        _entityCollections.Add(entityCollection);
    }
    
    protected void AddTickableSystem(ITickableSystem tickableSystem)
    {
        _tickableSystems.Add(tickableSystem);
    }
}