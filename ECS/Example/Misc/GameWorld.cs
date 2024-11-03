namespace ECS;

public sealed class GameWorld : World
{
    public readonly EntityCollection<MapEntity> MapEntities = new();
    public readonly EntityCollection<PlayerEntity> PlayerEntities = new();

    public GameWorld()
    {
        AddTickableSystem(new DrawWorldSystem(
            MapEntities,
            PlayerEntities
        ));
        
        AddEntityCollection(MapEntities);
        AddEntityCollection(PlayerEntities);
    }
}