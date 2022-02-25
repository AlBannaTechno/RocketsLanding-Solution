using RocketLanding.Infrastructure.Interfaces;

namespace RocketLanding.Infrastructure.Impl;

public record Position : IPositioned
{
    public int X { get; init; }
    public int Y { get; init; }
}