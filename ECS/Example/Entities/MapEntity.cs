namespace ECS;

public sealed class MapEntity : Entity
{
    public MapTilesComponent MapTiles;
    public MapBoundsComponent MapBounds;
    
    public MapEntity(int id) : base(id)
    {
        
    }

    protected override void WhenReset()
    {
        MapTiles = new MapTilesComponent();
        MapBounds = new MapBoundsComponent();
    }
}