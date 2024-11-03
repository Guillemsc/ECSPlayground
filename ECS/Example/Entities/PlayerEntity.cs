namespace ECS;

public sealed class PlayerEntity : Entity
{
    public MapPositionComponent MapPosition;
    
    public PlayerEntity(int id) : base(id)
    {
    }

    protected override void WhenReset()
    {
        MapPosition = new MapPositionComponent();
    }
}