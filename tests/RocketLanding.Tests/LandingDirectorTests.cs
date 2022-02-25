using FluentAssertions;
using RocketLanding.Infrastructure;
using RocketLanding.Infrastructure.Exceptions;
using Xunit;

namespace RocketLanding.Tests;

public class LandingDirectorTests
{

    [Theory]
    [InlineData(5, 10, 20, 40, 5 + 20 -1, 10 + 40 - 1)]
    public void ShouldSetPlatformValuesCorrectly(int x, int y, int width, int height, int maxX, int maxY)
    {
        var director = new LandingDirector();
        director.SetPlatform(x, y, width, height);
        
        director.LandingPlatform.Should().NotBeNull();
        
        director.LandingPlatform!.X.Should().Be(x);
        director.LandingPlatform!.Y.Should().Be(y);
        director.LandingPlatform!.Width.Should().Be(width);
        director.LandingPlatform!.Height.Should().Be(height);
        director.LandingPlatform!.MaxX.Should().Be(maxX);
        director.LandingPlatform!.MaxY.Should().Be(maxY);
    }
    
    [Theory]
    [InlineData(10, 20)]
    public void ShouldSetAreaValuesCorrectly(int width, int height)
    {
        var director = new LandingDirector();
        director.SetArea(width, height);
        
        director.LandingArea.Should().NotBeNull();
        
        director.LandingArea!.Width.Should().Be(width);
        director.LandingArea!.Height.Should().Be(height);
    }

    [Theory]
    [InlineData(-5, 10, 20, 40)]
    [InlineData(5, -10, 20, 40)]
    [InlineData(5, 10, 0, 40)]
    [InlineData(5, 10, -15, 40)]
    [InlineData(5, 10, 15, 0)]
    [InlineData(5, 10, 15, -40)]
    [InlineData(-5, -10, -15, -40)]
    public void ShouldGuardAgainstPlatformInvalidValues(int x, int y, int width, int height)
    {
        var director = new LandingDirector();
        this.Invoking(_ =>
        {
            director.SetPlatform(x, y, width, height);
        }).Should().Throw<InvalidLandingPlatformException>();
    }
    
    
    [Theory]
    [InlineData(0, 10)]
    [InlineData(-5, 10)]
    [InlineData(5, 0)]
    [InlineData(5, -10)]
    public void ShouldGuardAgainstAreaInvalidValues(int width, int height)
    {
        var director = new LandingDirector();
        this.Invoking(_ =>
        {
            director.SetArea(width, height);
        }).Should().Throw<InvalidAreaException>();
    }
    
    [Fact]
    public void ShouldProtectAgainstInvalidAreaAndPositionsValues()
    {
        var director = new LandingDirector();
        director.SetArea(100, 100);

        this.Invoking(_ => { director.SetPlatform(100, 55, 10, 10); }).Should().Throw<AreaNotContainedException>();
        this.Invoking(_ => { director.SetPlatform(98, 55, 1, 10); }).Should().NotThrow<AreaNotContainedException>();
        
        this.Invoking(_ => { director.SetArea(100, 100); }).Should().NotThrow<AreaNotContainedException>();
        this.Invoking(_ => { director.SetArea(98, 100); }).Should().Throw<AreaNotContainedException>();
    }

    [Fact]
    public void ShouldPreventClashingOnTheSamePosition()
    {
        var director = new LandingDirector();
        director.SetArea(100, 100);
        director.SetPlatform(5, 5, 10, 10);
        
        director.IsPositionAvailableForLanding(5, 6).Should().Be(LandingAvailability.Ok);
        director.IsPositionAvailableForLanding(5, 6).Should().Be(LandingAvailability.Clash);
    }
    
    [Theory]
    [InlineData(4, 2, LandingAvailability.OutOfPlatform)]
    [InlineData(8, 9, LandingAvailability.Ok)]
    [InlineData(9, 9, LandingAvailability.Clash)]
    [InlineData(10, 9, LandingAvailability.Clash)]
    [InlineData(11, 9, LandingAvailability.Clash)]
    [InlineData(9, 10, LandingAvailability.Clash)]
    [InlineData(10, 10, LandingAvailability.Clash)]
    [InlineData(11, 10, LandingAvailability.Clash)]
    [InlineData(9, 11, LandingAvailability.Clash)]
    [InlineData(10, 11, LandingAvailability.Clash)]
    [InlineData(11, 11, LandingAvailability.Clash)]
    [InlineData(11, 12, LandingAvailability.Ok)]
    public void ShouldPreventLandingOnThePreviousSelfArea(int x, int y, LandingAvailability status)
    {
        var director = new LandingDirector();
        director.SetArea(100, 100);
        director.SetPlatform(5, 5, 10, 10);
        director.IsPositionAvailableForLanding(10, 10).Should().Be(LandingAvailability.Ok);
        director.IsPositionAvailableForLanding(x, y).Should().Be(status);
    }
    
    [Theory]
    [InlineData(5, 5, LandingAvailability.Ok)]
    [InlineData(15, 15, LandingAvailability.OutOfPlatform)]
    [InlineData(14, 14, LandingAvailability.Ok)]
    [InlineData(3, 6, LandingAvailability.OutOfPlatform)]
    [InlineData(6, 3, LandingAvailability.OutOfPlatform)]
    public void ShouldHandleBasicLanding(int x, int y, LandingAvailability status)
    {
        var director = new LandingDirector();
        director.SetArea(100, 100);
        director.SetPlatform(5, 5, 10, 10);
        director.IsPositionAvailableForLanding(x, y).Should().Be(status);
    }
}