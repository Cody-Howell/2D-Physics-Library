using FluentAssertions;
using Physics2DLibrary;
using System.Net.WebSockets;
namespace Physics_2D_Library.Tests.LineClass;

public class LineClassBasicTests {
    [Fact]
    public void LineClassConstructorsTest() {
        Line2D l1 = new Line2D();
        Line2D l2 = new Line2D([1, 2, 3, 4]);
        Line2D l3 = new Line2D(l2);

        l3.UpdatePoint(0, 0, 2);
        l3.UpdatePoint(1, new Point2D(5, 6));
        l2[0].X.Should().Be(1);
        l3.Points.Should().Equal([new Point2D(0, 2), new Point2D(5, 6)]);
    }

    [Fact]
    public void RemainingPropertyTests() {
        Line2D l1 = new Line2D(new Point2D(0, 0), new Point2D(3, 4));
        l1.Length.Should().Be(5);
        l1.Midpoint.X.Should().Be(1.5);
        l1.Midpoint.Y.Should().Be(2);
        l1.Angle.RotationAngle.Should().Be(53.13);
    }
}
public class LineClassMinMaxTests {
    [Theory]
    [InlineData(1, 2, 3, 4, 1, 2, 3, 4)]
    [InlineData(4.0, 3.9, 3.1, 2.5, 3.1, 2.5, 4.0, 3.9)]
    [InlineData(3.5, -4.7, -3.7, 5.4, -3.7, -4.7, 3.5, 5.4)]
    [InlineData(-0.2, 5.1, -3.8, 5.5, -3.8, 5.1, -0.2, 5.5)]
    [InlineData(2.7, -2.6, -1.4, 1.0, -1.4, -2.6, 2.7, 1.0)]
    [InlineData(0.2, -4.7, -0.4, 5.6, -0.4, -4.7, 0.2, 5.6)]
    [InlineData(5.5, -3.5, 1.7, -3.7, 1.7, -3.7, 5.5, -3.5)]
    [InlineData(1.0, 4.1, 4.9, 1.2, 1.0, 1.2, 4.9, 4.1)]
    [InlineData(-0.7, -2.5, 1.9, 5.5, -0.7, -2.5, 1.9, 5.5)]
    [InlineData(-4.2, -3.9, -0.2, -4.9, -4.2, -4.9, -0.2, -3.9)]
    [InlineData(3.6, -4.7, -1.2, -3.7, -1.2, -4.7, 3.6, -3.7)]
    [InlineData(2.7, -3.3, 2.5, 4.5, 2.5, -3.3, 2.7, 4.5)]
    [InlineData(-2.4, -2.6, -3.3, 5.9, -3.3, -2.6, -2.4, 5.9)]
    [InlineData(2.4, 5.1, -2.4, 2.5, -2.4, 2.5, 2.4, 5.1)]
    [InlineData(5.1, 4.1, -4.4, 0.5, -4.4, 0.5, 5.1, 4.1)]
    [InlineData(1.2, 4.3, 1.0, -1.1, 1.0, -1.1, 1.2, 4.3)]
    [InlineData(2.4, -0.5, 3.3, 5.1, 2.4, -0.5, 3.3, 5.1)]
    [InlineData(0.7, 5.3, -0.7, 0.2, -0.7, 0.2, 0.7, 5.3)]
    [InlineData(4.9, 1.7, -3.0, -4.2, -3.0, -4.2, 4.9, 1.7)]
    [InlineData(-0.2, 1.7, 1.7, 2.7, -0.2, 1.7, 1.7, 2.7)]
    [InlineData(-2.8, 2.4, -3.3, -0.7, -3.3, -0.7, -2.8, 2.4)]
    public void AllMinMaxTests(
        double x1, double y1, double x2, double y2, double minX, double minY, double maxX, double maxY) {
        Line2D line = new Line2D(x1, y1, x2, y2);
        line.MinX.Should().Be(minX);
        line.MinY.Should().Be(minY);
        line.MaxX.Should().Be(maxX);
        line.MaxY.Should().Be(maxY);
    }
}
public class LineClassConnectionTests {
    [Theory]
    [InlineData(1, 2, 3, 4, 2, 3, 4, 5, false)]
    [InlineData(1, 2, 3, 4, 1, 2, 3, 4, false)]
    [InlineData(1, 2, 3, 4, 0, 5, 3, 4, true)]
    public void ConnectedLineTests(
        double l1x1, double l1y1, double l1x2, double l1y2,
        double l2x1, double l2y1, double l2x2, double l2y2, bool isConnected) {
        Line2D l1 = new Line2D(l1x1, l1y1, l1x2, l1y2);
        Line2D l2 = new Line2D(l2x1, l2y1, l2x2, l2y2);
        l1.IsConnected(l2).Should().Be(isConnected);
    }

    [Theory]
    [InlineData(1, 2, 3, 4, 2, 3, 4, 5, false)]
    [InlineData(1, 2, 3, 4, 1, 2, 3, 4, true)]
    [InlineData(1, 2, 3, 4, 0, 5, 3, 4, true)]
    public void ContainsEndpointTests(
    double l1x1, double l1y1, double l1x2, double l1y2,
    double l2x1, double l2y1, double l2x2, double l2y2, bool isConnected) {
        Line2D l1 = new Line2D(l1x1, l1y1, l1x2, l1y2);
        Line2D l2 = new Line2D(l2x1, l2y1, l2x2, l2y2);
        l1.ContainsEndpoint(l2).Should().Be(isConnected);
        (l1.ContainsEndpoint(l2[0]) || l1.ContainsEndpoint(l2[1])).Should().Be(isConnected);
    }
}
public class LineClassIntersectionAndPointTests {
    [Theory] // All these were validated through Desmos
    [InlineData(-3.6, 0.5, 0, -4.0, 2.5, 3.8,  -2.9, -2.1, true)]
    [InlineData(-1.6, -2.2, 5.1, -0.2, -0.9, 2.8, 1.5, -2.3, true)]
    [InlineData(-2.3, 2.7, -2.2, 5.5, 5.6, 0.7, 0.2, -3.6, false)]
    [InlineData(-1.1, 5, 2.5, 0.5, -4.2, 4.9, 0.5, 4.0, true)]
    [InlineData(0.7, -3.8, -1.9, 0.2, -1.5, -2.2, -0.4, 2.0, true)]
    [InlineData(1.9, 2.3, -2.2, 0.4, 5.1, -1.7, -2.1, 0.0, false)]
    [InlineData(-2.3, -2.5, 1.5, 5.4, 4.6, 3.9, 5.6, 5.0, false)]
    [InlineData(1.5, 4.9, -2.2, -0.5, 4.5, -1.9, -3.4, 4.6, true)]
    [InlineData(-4.5, -1.1, 3.8, 1.2, 5.1, -1.2, 1.2, 4.3, true)]
    [InlineData(4.1, -5.0, 5.1, -3.1, -0.5, -4.0, 3.9, -0.7, false)]
    public void IntersectionTests(
        double l1x1, double l1y1, double l1x2, double l1y2,
        double l2x1, double l2y1, double l2x2, double l2y2, bool isIntersecting) {
        Line2D l1 = new Line2D(l1x1, l1y1, l1x2, l1y2);
        Line2D l2 = new Line2D(l2x1, l2y1, l2x2, l2y2);
        l1.IsIntersecting(l2).Should().Be(isIntersecting);
        (l1 ^ l2).Should().Be(isIntersecting);

        Vector2D vector = new Vector2D();
        vector.AssignToCoordinates(l2x1, l2y1, l2x2, l2y2);
        l1.IsIntersecting(new Point2D(l2x1, l2y1), vector).Should().Be(isIntersecting);
    }

    [Theory]
    [InlineData(-3.6, 0.5, 0, -4.0, 2.5, 3.8, -2.9, -2.1, -2.2, -1.3, true)]
    [InlineData(-1.6, -2.2, 5.1, -0.2, -0.9, 2.8, 1.5, -2.3, 1.1, -1.4, true)]
    [InlineData(-2.3, 2.7, -2.2, 5.5, 5.6, 0.7, 0.2, -3.6, -2.6, -5.8, false)]
    public void IntersectionPointTests(
        double l1x1, double l1y1, double l1x2, double l1y2,
        double l2x1, double l2y1, double l2x2, double l2y2, double pointX, double pointY, bool intersects) {
        Line2D l1 = new Line2D(l1x1, l1y1, l1x2, l1y2);
        Line2D l2 = new Line2D(l2x1, l2y1, l2x2, l2y2);
        Point2D? answer = Line2D.IntersectionPoint(l1, l2);

        if (answer is not null) {
            answer.X.Should().BeApproximately(pointX, 0.1);
            answer.Y.Should().BeApproximately(pointY, 0.1);
            intersects.Should().BeTrue();
        } else {
            intersects.Should().BeFalse();
        }
    }

    [Theory]
    [InlineData(-3.6, 0.5, 0, -4.0, -2.2, -1.3, true)]
    [InlineData(-1.6, -2.2, 5.1, -0.2, 1.1, -1.4, true)]
    [InlineData(-2.3, 2.7, -2.2, 5.5, -2.6, -5.8, false)]
    public void ContainsPointTests(
        double l1x1, double l1y1, double l1x2, double l1y2,
        double pointX, double pointY, bool containsPoint) {
        Line2D l1 = new Line2D(l1x1, l1y1, l1x2, l1y2);
        l1.ContainsPoint(pointX, pointY).Should().Be(containsPoint);
        l1.ContainsPoint(new Point2D(pointX, pointY)).Should().Be(containsPoint);
    }
}




