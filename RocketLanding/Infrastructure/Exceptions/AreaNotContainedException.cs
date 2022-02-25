using RocketLanding.Infrastructure.Interfaces;

namespace RocketLanding.Infrastructure.Exceptions;

public class AreaNotContainedException : Exception
{
    public AreaNotContainedException(IPositionedArea positionedArea, IArea area)
    {
        Area = area;
        PositionedArea = positionedArea;
    }

    public IArea Area { get; set; }
    public IPositionedArea PositionedArea { get; set; }
}