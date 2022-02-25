using RocketLanding.Components;
using RocketLanding.Infrastructure;
using RocketLanding.Infrastructure.Exceptions;
using RocketLanding.Infrastructure.Impl;
using RocketLanding.Infrastructure.Interfaces;
using RocketLanding.Infrastructure.Utils;

namespace RocketLanding;

public class LandingDirector : ILandingDirector
{
    protected internal LandingArea? LandingArea;
    protected internal LandingPlatform? LandingPlatform;
    public Position? LastLandingPosition { get; private set; }

    public void SetArea(int width, int height)
    {
        var area = new LandingArea {Width = width, Height = height};
        if (!area.IsValid)
        {
            throw new InvalidAreaException{Area = area};
        }
        if (LandingPlatform is not null)
        {
            if (!GeometryUtils.IsPositionedAreaContainedInArea(LandingPlatform, area))
            {
                throw new AreaNotContainedException(LandingPlatform, area);
            }
        }
        LandingArea = area;
    }

    public void SetPlatform(int x, int y, int width, int height)
    {
        var platform = new LandingPlatform{X = x, Y = y, Width = width, Height = height};
        if (!platform.IsValid)
        {
            throw new InvalidLandingPlatformException{LandingPlatform = platform};
        }
        if (LandingArea is not null)
        {
            if (!GeometryUtils.IsPositionedAreaContainedInArea(platform, LandingArea))
            {
                throw new AreaNotContainedException(platform, LandingArea);
            }
        }
        LandingPlatform = platform;
    }

    public LandingAvailability IsPositionAvailableForLanding(int x, int y)
    {
        var requestedPosition = new Position {X = x, Y = y};
        
        if (LandingArea is null || LandingPlatform is null)
        {
            throw new NullReferenceException();
        }
        
        if (x < 0 || y < 0)
        {
            return LandingAvailability.OutOfPlatform;
        }

        if (x >= LandingArea.Width || y >= LandingArea.Height)
        {
            return LandingAvailability.OutOfPlatform;
        }

        if (!GeometryUtils.IsPositionedContainedInPositionedArea(LandingPlatform, requestedPosition))
        {
            return LandingAvailability.OutOfPlatform;
        }
        
        if (LastLandingPosition is not null)
        {
            if (LastLandingPosition == requestedPosition)
            {
                return LandingAvailability.Clash;
            }
            if (GeometryUtils.IsPositionedInSelfArea(LastLandingPosition, requestedPosition))
            {
                return LandingAvailability.Clash;
            }
        }

        LastLandingPosition = new Position {X = x, Y = y};
        return LandingAvailability.Ok;
    }
}