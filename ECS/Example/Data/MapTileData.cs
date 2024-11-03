using System.Numerics;

namespace ECS.Example.Data;

public sealed class MapTileData
{
    public Vector2 Position { get; }
    public bool Walkable { get; }
    
    public MapTileData(Vector2 position, bool walkable)
    {
        Walkable = walkable;
        Position = position;
    }
}