namespace ECS;

public abstract class Entity 
{
    public int Id { get; private set; }

    public bool IsValid => Id >= 0;
    
    protected Entity(int id)
    {
        Id = id;
    }

    public void Reset(int id)
    {
        Id = id;
        
        WhenReset();
    }

    public void Invalidate()
    {
        Id = -1;
    }

    protected abstract void WhenReset();
}