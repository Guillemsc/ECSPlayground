using System.Numerics;

namespace ECS.Example.Utils;

public static class InputUtils
{
    public static void ProcessInput(GameWorld gameWorld, ConsoleKey key)
    {
        MapEntity? mapEntity = gameWorld.MapEntities.Entities.FirstOrDefault();
        PlayerEntity? playerEntity = gameWorld.PlayerEntities.Entities.FirstOrDefault();

        if (mapEntity == null)
        {
            return;
        }
        
        if (playerEntity == null)
        {
            return;
        }
        
        Vector2 movement = Vector2.Zero;

        switch (key)
        {
            case ConsoleKey.W:
            {
                movement = new Vector2(0, -1);
                break;
            }
            
            case ConsoleKey.A:
            {
                movement = new Vector2(-1, 0);
                break;
            }
            
            case ConsoleKey.S:
            {
                movement = new Vector2(0, 1);
                break;
            }
            
            case ConsoleKey.D:
            {
                movement = new Vector2(1, 0);
                break;
            }
        }
        
        Vector2 newPosition = playerEntity.MapPosition.Position + movement;

        bool canMove = mapEntity.MapTiles.WalkableTiles.Contains(newPosition);

        if (canMove)
        {
            playerEntity.MapPosition.Position = newPosition;
        }
    }
}