using System.Numerics;
using ECS.Example.Data;

namespace ECS.Example.Utils;

public static class MapUtils
{
    public static void ProcesMap(
        ref MapTilesComponent mapTiles, 
        List<MapTileData> mapTileDatas
        )
    {
        Dictionary<Vector2, MapTileData> allTiles = new();
        HashSet<Vector2> walkableTiles = new();
        
        foreach (MapTileData mapTileData in mapTileDatas)
        {
            allTiles.Add(mapTileData.Position, mapTileData);

            if (mapTileData.Walkable)
            {
                walkableTiles.Add(mapTileData.Position);
            }
        }
        
        HashSet<Vector2> borderPositions = new();

        foreach (Vector2 position in walkableTiles)
        {
            List<Vector2> surroundingPositions = GetSurroundingPositions(position);

            foreach (Vector2 surroundingPosition in surroundingPositions)
            {
                bool isWalkable = walkableTiles.Contains(surroundingPosition);

                if (isWalkable)
                {
                    continue;
                }

                borderPositions.Add(surroundingPosition);
            }
        }

        mapTiles.AllTiles = allTiles;
        mapTiles.WalkableTiles = walkableTiles;
        mapTiles.BorderPositions = borderPositions;
    }
    
    public static void SetMapBounds(ref MapTilesComponent mapTiles, ref MapBoundsComponent mapBounds)
    {
        Vector2 min = new Vector2(int.MaxValue, int.MaxValue);
        Vector2 max = new Vector2(int.MinValue, int.MinValue);
        
        foreach (Vector2 position in mapTiles.AllTiles.Keys)
        {
            min = Vector2.Min(min, position);
            max = Vector2.Max(max, position);
        }
        
        mapBounds.Min = min;
        mapBounds.Max = max;
        mapBounds.Size = max - min;
    }
    
    public static List<Vector2> GetSurroundingPositions(Vector2 position)
    {
        List<Vector2> ret = new List<Vector2>()
        {
            position with { X = position.X + 1 },
            position with { X = position.X - 1 },
            position with { Y = position.Y + 1 },
            position with { Y = position.Y - 1 },
            position with { X = position.X + 1, Y = position.Y + 1 },
            position with { X = position.X + 1, Y = position.Y - 1 },
            position with { X = position.X - 1, Y = position.Y + 1 },
            position with { X = position.X - 1, Y = position.Y - 1 },
        };

        return ret;
    }
}