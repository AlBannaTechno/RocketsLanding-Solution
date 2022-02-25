using RocketLanding.Infrastructure.Impl;
using RocketLanding.Infrastructure.Interfaces;

namespace RocketLanding.Infrastructure.Utils;

public static class GeometryUtils
{
    public static bool IsPositionedAreaContainedInArea(IPositionedArea positionedArea, IArea area)
    {
        return positionedArea.MaxX <= area.Width - 1 && positionedArea.MaxY <= area.Height - 1;
    }
    
    public static bool IsPositionedContainedInPositionedArea(IPositionedArea positionedArea, IPositioned positioned)
    {
        return
            positioned.X <= positionedArea.MaxX 
            && positioned.X >= positionedArea.X 
            && positioned.Y <= positionedArea.MaxY 
            && positioned.Y >= positionedArea.Y;
    }

    public static bool IsPositionedInSelfArea(Position resident, Position visitor, int horizontalMargin = 1, int verticalMargin = 1)
    {
        var x1 = resident.X - horizontalMargin;
        var y1 = resident.Y - verticalMargin;
        var x2 = resident.X + horizontalMargin;
        var y2 = resident.Y + verticalMargin;
        return visitor.X >= x1 && visitor.X <= x2 && visitor.Y >= y1 && visitor.Y <= y2;
    }
}