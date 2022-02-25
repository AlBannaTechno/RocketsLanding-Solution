using RocketLanding.Infrastructure.Interfaces;

namespace RocketLanding.Infrastructure.Exceptions;

public class InvalidAreaException : Exception
{
    public IArea Area { get; set; }
}