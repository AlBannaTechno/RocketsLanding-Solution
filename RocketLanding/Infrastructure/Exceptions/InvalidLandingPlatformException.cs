using RocketLanding.Components;

namespace RocketLanding.Infrastructure.Exceptions;

public class InvalidLandingPlatformException : Exception
{
    public LandingPlatform LandingPlatform { get; set; } 
}