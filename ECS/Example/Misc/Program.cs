// See https://aka.ms/new-console-template for more information

using System.Numerics;
using ECS;
using ECS.Example.Data;
using ECS.Example.Utils;

Console.WriteLine("Start :)");

GameWorld gameWorld = new();

MapEntity mapEntity = gameWorld.MapEntities.CreateEntity();

MapUtils.ProcesMap(
    ref mapEntity.MapTiles,
    new List<MapTileData>()
    {
        new(new Vector2(0, 0), true),
        new(new Vector2(1, 0), true),
        new(new Vector2(2, 0), true),
        new(new Vector2(1, 1), true),
        new(new Vector2(2, 1), true),
        new(new Vector2(3, 1), true),
        new(new Vector2(4, 1), true),
        new(new Vector2(5, 1), true),
    });

MapUtils.SetMapBounds(ref mapEntity.MapTiles, ref mapEntity.MapBounds);

PlayerEntity playerEntity = gameWorld.PlayerEntities.CreateEntity();

ConsoleKeyInfo? consoleKeyInfo = null;

while (consoleKeyInfo == null || consoleKeyInfo!.Value.Key != ConsoleKey.Q)
{
    gameWorld.Tick();

    consoleKeyInfo = Console.ReadKey();
    
    InputUtils.ProcessInput(gameWorld, consoleKeyInfo.Value.Key);
}

gameWorld.Dispose();