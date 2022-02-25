using RocketLanding.Infrastructure.Impl;

namespace RocketLanding.Infrastructure.Interfaces;

public interface ILandingDirector
{
    Position? LastLandingPosition { get; }
    void SetArea(int width, int height);
    void SetPlatform(int x, int y, int width, int height);
    LandingAvailability IsPositionAvailableForLanding(int x, int y);
}