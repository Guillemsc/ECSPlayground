using System.Numerics;

namespace ECS;

public record struct MapBoundsComponent(Vector2 Min, Vector2 Max, Vector2 Size);