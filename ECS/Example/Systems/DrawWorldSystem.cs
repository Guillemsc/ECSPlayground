using System.Numerics;
using System.Text;
using ECS.Example.Utils;

namespace ECS;

public readonly struct DrawWorldSystem : ITickableSystem
{
    readonly EntityCollection<MapEntity> _mapCollection;
    readonly EntityCollection<PlayerEntity> _playerCollection;

    public DrawWorldSystem(
        EntityCollection<MapEntity> mapCollection, 
        EntityCollection<PlayerEntity> playerCollection
        )
    {
        _mapCollection = mapCollection;
        _playerCollection = playerCollection;
    }

    public void Tick()
    {
        MapEntity? mapEntity = _mapCollection.Entities.FirstOrDefault();

        if (mapEntity == null)
        {
            return;
        }

        PlayerEntity? playerEntity = _playerCollection.Entities.FirstOrDefault();
        Vector2? playerPosition = playerEntity?.MapPosition.Position;

        Vector2 arrayOffset = -mapEntity.MapBounds.Min + Vector2.One;

        char[,] mapChars = new char[(int)mapEntity.MapBounds.Size.X + 3, (int)mapEntity.MapBounds.Size.Y + 3];
        
        for (int column = 0; column < mapChars.GetLength(1); column++)
        {
            for (int row = 0; row < mapChars.GetLength(0); row++)
            {
                Vector2 arrayPosition = new Vector2(row, column);
                Vector2 mapPosition = arrayPosition - arrayOffset;

                bool isBorder = mapEntity.MapTiles.BorderPositions.Contains(mapPosition);
                bool isWalkable = mapEntity.MapTiles.WalkableTiles.Contains(mapPosition);
                bool isPlayerPosition = playerPosition != null && playerPosition.Value == mapPosition;

                char charToSet = ' ';

                if (isPlayerPosition)
                {
                    charToSet = 'P';
                }  
                else if (isBorder)
                {
                    charToSet = '#';
                }
                else if (isWalkable)
                {
                    charToSet = ' ';
                }
                
                mapChars[row, column] = charToSet;
            }
        }
        
        StringBuilder stringBuilder = new();

        for (int column = 0; column < mapChars.GetLength(1); column++)
        {
            for (int row = 0; row < mapChars.GetLength(0); row++)
            {
                stringBuilder.Append(mapChars[row, column]);
            }
            
            stringBuilder.AppendLine();
        }

        Console.WriteLine(stringBuilder);
    }
}