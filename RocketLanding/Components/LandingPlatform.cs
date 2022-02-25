using RocketLanding.Infrastructure.Interfaces;

namespace RocketLanding.Components;

public class LandingPlatform : IPositionedArea
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int X { get; init; }
    public int Y { get; init; }

    public int MaxX => X + Width - 1;
    public int MaxY => Y + Height - 1;

    public bool IsValid => Width > 0 && Height > 0 && X >= 0 && Y >= 0;
}