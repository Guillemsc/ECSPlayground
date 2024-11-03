using System.Numerics;
using ECS.Example.Data;

namespace ECS;

public record struct MapTilesComponent(
    Dictionary<Vector2, MapTileData> AllTiles,
    HashSet<Vector2> WalkableTiles,
    HashSet<Vector2> BorderPositions
);