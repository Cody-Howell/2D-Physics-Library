using FluentAssertions;
namespace Physics2DLibrary.Tests.VectorClass;

public class VectorClassBasicTests {
    [Fact]
    public void DefaultConstructorTest() {
        Vector2D m1 = new Vector2D();
        m1.Rotation.RotationAngle.Should().Be(0);
        m1.Velocity.Should().Be(0.0);
    }

    [Fact]
    public void SimpleConstructorTest() {
        Vector2D m1 = new Vector2D(25, 2.3);
        m1.Rotation.RotationAngle.Should().Be(25);
        m1.Velocity.Should().Be(2.3);
    }

    [Fact]
    public void ObjectConstructorTest() {
        Vector2D m1 = new Vector2D(new Rotation2D(25), 2.3);
        m1.Rotation.RotationAngle.Should().Be(25);
        m1.Velocity.Should().Be(2.3);
    }

    [Fact]
    public void UpdateTests() {
        Vector2D m1 = new Vector2D();
        m1.UpdateRotation(15);
        m1.UpdateVelocity(2.2);
        m1.Rotation.RotationAngle.Should().Be(15);
        m1.Velocity.Should().Be(2.2);

        m1.UpdateRotation(new Rotation2D(25));
        m1.Rotation.RotationAngle.Should().Be(25);
    }
}
public class VectorClassOperatorTests {
    [Theory]
    [InlineData(45, 2.2, 45, 2.2, true)]
    [InlineData(45, 2.2, 85, 2.2, false)]
    [InlineData(45, 2.3, 45, 2.2, false)]
    public void EqualityOperators(
        int rotAngle1, double velocity1, int rotAngle2, double velocity2, bool shouldBeEqual) {
        Vector2D m1 = new Vector2D(rotAngle1, velocity1);
        Vector2D m2 = new Vector2D(rotAngle2, velocity2);

        m1.Equals(m2).Should().Be(shouldBeEqual);
        (m1 == m2).Should().Be(shouldBeEqual);
        (m1 != m2).Should().Be(!shouldBeEqual);
    }

    [Theory] // Validated with Desmos
    [InlineData(-0.2, -2.7, 162, 1.05, -1.2, -2.4)]
    [InlineData(0.7, 0.7, 343, 2.69, 3.3, 0)]
    [InlineData(-0.7, 2.4, 327, 2.49, 1.4, 1.1)]
    [InlineData(-1.3, -0.1, 189, 3.89, -5.2, -0.7)]
    [InlineData(-1.4, 1.7, 87, 0.35, -1.4, 2.0)]
    [InlineData(1.4, -2.3, 80, 0.13, 1.4, -2.2)]
    [InlineData(-2.1, -2.2, 184, 1.25, -3.4, -2.3)]
    [InlineData(2.8, 1.9, 274, 2.51, 3.0, -0.6)]
    [InlineData(3.2, -0.2, 157, 3.83, -0.3, 1.3)]
    [InlineData(3.2, -2.0, 49, 2.88, 5.1, 0.2)]
    public void AddOperator(
        double pointX, double pointY, int rotAngle, double velocity,
        double answerX, double answerY) {
        Point2D p = new Point2D(pointX, pointY);
        Vector2D m = new Vector2D(rotAngle, velocity);

        Point2D answer = p + m;
        answer.X.Should().BeApproximately(answerX, 0.1);
        answer.Y.Should().BeApproximately(answerY, 0.1);
    }

    [Theory] // Validated with Desmos
    [InlineData(-0.2, -2.7, 162, 1.05, 0.8, -3.0)]
    [InlineData(0.7, 0.7, 343, 2.69, -1.8, 1.48)]
    [InlineData(-0.7, 2.4, 327, 2.49, -2.8, 3.7)]
    [InlineData(-1.3, -0.1, 189, 3.89, 2.5, 0.5)]
    [InlineData(-1.4, 1.7, 87, 0.35, -1.4, 1.4)]
    [InlineData(1.4, -2.3, 80, 0.13, 1.4, -2.4)]
    [InlineData(-2.1, -2.2, 184, 1.25, -0.8, -2.1)]
    [InlineData(2.8, 1.9, 274, 2.51, 2.6, 4.4)]
    [InlineData(3.2, -0.2, 157, 3.83, 6.7, -1.7)]
    [InlineData(3.2, -2.0, 49, 2.88, 1.3, -4.16)]
    public void SubtractOperator(
    double pointX, double pointY, int rotAngle, double velocity,
    double answerX, double answerY) {
        Point2D p = new Point2D(pointX, pointY);
        Vector2D m = new Vector2D(rotAngle, velocity);

        Point2D answer = p - m;
        answer.X.Should().BeApproximately(answerX, 0.1);
        answer.Y.Should().BeApproximately(answerY, 0.1);
    }
}
public class VectorClassComparisonTests {
    [Theory]
    [InlineData(45, 0, 45, 2, -1)]
    [InlineData(45, 0, 45, -2, 1)]
    [InlineData(45, 0, 45, 0, 0)]
    [InlineData(85, 0, 45, 0, 1)]
    [InlineData(45, 0, 85, 0, -1)]
    public void ComparisonTests(
        int rot1, double vel1, int rot2, double vel2, int compairsonValue) {
        Vector2D m1 = new Vector2D(rot1, vel1);
        Vector2D m2 = new Vector2D(rot2, vel2);

        m1.CompareTo(m2).Should().Be(compairsonValue);
    }
}
public class VectorClassCoordinateAssignmentTests {
    [Theory]
    [InlineData(1, 0, 0, 1)]
    [InlineData(0, 1, 90, 1)]
    [InlineData(-1, 0, 180, 1)]
    [InlineData(0, -1, 270, 1)]
    public void SimpleCoordinateTests(
        double x, double y, int outRotation, double outVelocity) {
        Vector2D m = new Vector2D();
        m.AssignToCoordinates(x, y);
        m.Rotation.RotationAngle.Should().Be(outRotation);
        m.Velocity.Should().Be(outVelocity);
    }

    [Theory]
    [InlineData(8.3, 1.5, 10.24, 8.4)]
    [InlineData(8.8, 2.5, 15.86, 9.1)]
    [InlineData(6.8, 4.8, 35.22, 8.3)]
    [InlineData(0.5, 3.5, 81.87, 3.5)]
    [InlineData(0.8, 7.5, 83.91, 7.5)]
    [InlineData(-0.6, 10.7, 93.21, 10.7)]
    [InlineData(-1.0, 10.2, 95.6, 10.2)]
    [InlineData(-1.3, 5.1, 104.3, 5.2)]
    [InlineData(-3.7, 5.3, 124.92, 6.4)]
    [InlineData(-6.8, 6.6, 135.86, 9.5)]
    [InlineData(-6.1, 5.5, 137.96, 8.2)]
    [InlineData(-9.6, 3.0, 162.65, 10.0)]
    [InlineData(-7.0, -4.2, 210.96, 8.1)]
    [InlineData(-9.3, -5.7, 211.5, 10.9)]
    [InlineData(-10.0, -8.8, 221.35, 13.3)]
    [InlineData(-2.4, -8.8, 254.74, 9.1)]
    [InlineData(-2.0, -7.8, 255.62, 8.0)]
    [InlineData(6.8, -9.5, 305.59, 11.6)]
    [InlineData(6.6, -3.5, 332.06, 7.5)]
    [InlineData(8.5, -3.0, 340.56, 9.0)]
    public void ComplexCoordinateTests(
    double x, double y, double outRotation, double outVelocity) {
        Vector2D m = new Vector2D();
        m.AssignToCoordinates(new Point2D(x, y));

        m.Rotation.RotationAngle.Should().Be(outRotation);
        m.Velocity.Should().BeApproximately(outVelocity, 0.1);
    }

    [Theory]
    [InlineData(-1.85, 2.42, 2.08, -0.62, 322.28, 4.96)]
    [InlineData(1.88, -0.46, -1.25, -3.37, 222.91, 4.27)]
    [InlineData(-2.13, 1.33, 5.25, 0.54, 353.89, 7.42)]
    [InlineData(-2.68, -4.67, 4.83, 5.10, 52.45, 12.32)]
    [InlineData(-3.55, 5.65, -0.82, 1.17, 301.36, 5.24)]
    [InlineData(-3.65, 5.92, -4.80, -3.65, 263.15, 9.63)]
    [InlineData(4.40, -2.28, -3.61, 3.42, 144.56, 9.83)]
    [InlineData(-4.92, -0.95, 3.17, 3.49, 28.76, 9.22)]
    [InlineData(4.98, -2.66, -4.10, 0.75, 159.42, 9.69)]
    [InlineData(5.18, 4.73, 3.38, -1.92, 254.85, 6.88)]
    public void TwoPointCoordinateTests(
        double x1, double y1, double x2, double y2, double outRotation, double outVelocity) {
        Vector2D vector = new Vector2D();
        vector.AssignToCoordinates(x1, y1, x2, y2);
        vector.Rotation.RotationAngle.Should().Be(outRotation);
        vector.Velocity.Should().BeApproximately(outVelocity, 0.01);

        vector = new Vector2D();
        vector.AssignToCoordinates(new Point2D(x1, y1), new Point2D(x2, y2));
        vector.Rotation.RotationAngle.Should().Be(outRotation);
        vector.Velocity.Should().BeApproximately(outVelocity, 0.01);
    }
}


