using RocketLanding.Infrastructure.Interfaces;

namespace RocketLanding.Components;

public class LandingArea : IArea
{
    public int Width { get; set; }
    public int Height { get; set; }

    public bool IsValid => Width > 0 && Height > 0;
}