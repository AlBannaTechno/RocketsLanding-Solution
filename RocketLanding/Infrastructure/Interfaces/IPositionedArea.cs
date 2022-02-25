namespace RocketLanding.Infrastructure.Interfaces;

public interface IPositionedArea : IArea, IPositioned
{
    int MaxX { get; }
    int MaxY { get; }
}