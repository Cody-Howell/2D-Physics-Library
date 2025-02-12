using FluentAssertions;
using Physics2DLibrary;
namespace Physics_2D_Library.Tests.EquationClass;

public class EquationBasicTests {
    [Fact]
    public void DefaultConstructorTest() {
        Equation2D e = new Equation2D();
        e[0].Should().Be(0.0);
    }

    [Fact]
    public void PointConstructorTest() {
        Equation2D e = new Equation2D(new Point2D(0, 0), new Point2D(5, 5));
        e[1].Should().Be(1.0);
        e[0].Should().Be(0.0);
    }

    [Fact]
    public void LineConstructorTest() {
        Line2D line = new Line2D(new Point2D(0, 0), new Point2D(5, 5));
        Equation2D e = new Equation2D(line);
        e[1].Should().Be(1.0);
        e[0].Should().Be(0.0);
    }

    [Fact]
    public void CoefficientTest() {
        Equation2D e = new Equation2D(1, 4);
        e.Coefficients.Should().BeEquivalentTo(new double[] { 4, 1 });
    }

    [Fact]
    public void EquationCanBeCopied() {
        Equation2D e1 = new Equation2D(15, 10);
        Equation2D e2 = new Equation2D(e1);

        e2.UpdateCoefficient(0, 25);
        e1[0].Should().Be(10);
        e2[0].Should().Be(25);
    }
}
public class EquationCoordinateAssignmentTests {
    [Theory]
    [InlineData(0, 0, 5, 0, 0, 0)]
    [InlineData(0, 0, 5, 5, 1, 0)]
    [InlineData(0, 5, 5, 10, 1, 5)]
    [InlineData(0, 0, -5, 5, -1, 0)]
    [InlineData(0, 5, -5, 10, -1, 5)]
    [InlineData(0, -3, -2, -2, -0.5, -3)]
    public void SimpleAssignmentTests(
        double x1, double y1, double x2, double y2, double slope, double intercept) {
        Equation2D equation = new Equation2D(x1, y1, x2, y2);
        equation[0].Should().Be(intercept);
        equation[1].Should().Be(slope);
    }

    [Theory]
    [InlineData(3.43, 5.44, -1.83, 3.83, 0.30, 4.41)]
    [InlineData(3.60, -1.29, -4.23, 2.26, -0.45, 0.32)]
    [InlineData(3.47, 1.65, -4.63, -3.48, 0.63, -0.53)]
    [InlineData(1.71, 3.01, 1.16, -0.26, 5.87, -7.07)]
    [InlineData(1.09, -0.09, -0.59, -2.59, 1.46, -1.70)]
    [InlineData(-0.29, 5.17, 4.60, -1.10, -1.28, 4.78)]
    [InlineData(-2.66, 3.96, -1.90, -2.80, -8.89, -19.68)]
    [InlineData(-2.29, 3.42, -2.76, -1.08, 9.59, 25.39)]
    [InlineData(5.47, 0.73, 5.76, -2.75, -12.00, 66.37)]
    [InlineData(4.77, -4.43, -4.21, -3.99, -0.04, -4.23)]
    [InlineData(-4.55, -4.71, -0.42, 0.82, 1.34, 1.38)]
    [InlineData(3.19, 0.33, -3.83, -4.25, 0.65, -1.73)]
    [InlineData(0.91, 2.03, -2.78, -3.13, 1.39, 0.76)]
    [InlineData(-1.23, -0.63, -3.50, -3.44, 1.23, 0.87)]
    [InlineData(-2.81, -0.42, 5.02, -4.32, -0.49, -1.80)]
    [InlineData(5.55, 4.32, -1.10, -0.42, 0.71, 0.37)]
    [InlineData(2.29, 4.35, -3.38, 5.23, -0.15, 4.69)]
    [InlineData(5.89, -1.72, 3.55, 0.59, -0.99, 4.11)]
    [InlineData(0.67, 0.48, 2.11, 0.12, -0.24, 0.64)]
    [InlineData(5.47, -2.41, -4.69, 3.91, -0.62, 0.98)]
    public void ComplexAssignmentTests(
        double x1, double y1, double x2, double y2, double slope, double intercept) {
        Equation2D equation = new Equation2D();
        equation.AssignToCoordinates(x1, y1, x2, y2);
        equation[0].Should().BeApproximately(intercept, 0.1);
        equation[1].Should().BeApproximately(slope, 0.1);

        equation.AssignToCoordinates(new Point2D(x1, y1), new Point2D(x2, y2));
        equation[0].Should().BeApproximately(intercept, 0.1);
        equation[1].Should().BeApproximately(slope, 0.1);

        equation.AssignToCoordinates(new Line2D(x1, y1, x2, y2));
        equation[0].Should().BeApproximately(intercept, 0.1);
        equation[1].Should().BeApproximately(slope, 0.1);
    }
}
public class EquationAssignToPointTests {
    [Theory]
    [InlineData(5, 5, 0)]
    [InlineData(1.18, 7.53, 6.35)]
    [InlineData(7.75, 5.84, -1.91)]
    [InlineData(1.35, 8.16, 6.80)]
    [InlineData(2.99, 1.41, -1.58)]
    [InlineData(-8.12, -1.43, 6.68)]
    [InlineData(2.39, 7.69, 5.30)]
    [InlineData(8.48, 8.43, -0.05)]
    [InlineData(-2.36, 5.50, 7.86)]
    [InlineData(-3.30, -8.09, -4.78)]
    [InlineData(-6.85, -0.08, 6.76)]

    public void SlopeIsOne(
        double x, double y, double expIntercept) {
        Equation2D e = new Equation2D(1, 0);
        e.AssignToPoint(x, y);
        e.Intercept.Should().BeApproximately(expIntercept, 0.02);
        e.Slope.Should().Be(1);
        e.PointIsOnLine(x, y).Should().BeTrue();
    }

    [Theory]
    [InlineData(-9.60, 2.35, -1.9, -15.87)]
    [InlineData(-4.51, -0.82, -2.6, -12.54)]
    [InlineData(-6.91, -4.27, 4.9, 29.58)]
    [InlineData(5.42, -2.41, -4.7, 23.06)]
    [InlineData(-8.45, 6.35, 0.0, 6.35)]
    [InlineData(2.22, 0.16, -1.4, 3.26)]
    [InlineData(-9.50, 5.00, -2.9, -22.55)]
    [InlineData(9.48, 0.55, 0.5, -4.19)]
    [InlineData(-4.89, 10.57, 1.0, 15.46)]
    [InlineData(-9.04, -1.33, -4.2, -39.30)]
    public void VariableSlope(
        double x, double y, double slope, double expIntercept) {
        Equation2D e = new Equation2D(slope, 0);
        e.AssignToPoint(new Point2D(x, y));
        e.Intercept.Should().BeApproximately(expIntercept, 0.02);
        e.Slope.Should().Be(slope);
        e.PointIsOnLine(x, y).Should().BeTrue();
    }
}
public class EquationPointIsOnLineTests {
    [Theory]
    [InlineData(0, 0, 5, 0, 0, 0, true)]
    [InlineData(0, 0, 5, 5, 100, 100, true)]
    [InlineData(0, 0, 5, 5, 0, 1, false)]
    [InlineData(0, 5, 5, 10, 5, 5, false)]
    [InlineData(0, 0, -5, 5, -100, 100, true)]
    [InlineData(0, 0, -5, 5, -50, 49, false)]
    [InlineData(0, 5, -5, 10, 0, 5, true)]
    [InlineData(0, 5, -5, 10, 0, 6, false)]
    [InlineData(0, -3, -2, -2, -6, 0, true)]
    public void SimpleEquationPointTests(
        double x1, double y1, double x2, double y2, double pointX, double pointY, bool isOnLine) {
        Equation2D equation = new Equation2D(new Point2D(x1, y1), new Point2D(x2, y2));
        Point2D p = new Point2D(pointX, pointY);

        equation.PointIsOnLine(p).Should().Be(isOnLine);
    }

    [Theory]
    [InlineData(3.24, 5.50, 2.21, -4.60, -3.0, 2.90, false)]
    [InlineData(-0.32, 2.96, 5.06, -1.63, 0.0, -2.70, false)]
    [InlineData(2.30, -1.13, 5.33, 3.23, 3.9, 1.16, true)]
    [InlineData(-3.92, 1.54, 3.82, 1.63, 3.0, 0.39, false)]
    [InlineData(-4.32, 3.02, 2.04, 3.64, 0.7, -2.90, false)]
    [InlineData(0.67, -3.44, 5.68, -3.59, 0.5, -2.20, false)]
    [InlineData(3.01, 1.25, 1.17, -3.11, -3.0, -12.89, true)]
    [InlineData(2.76, 4.18, -0.79, -1.14, 2.2, 3.33, true)]
    [InlineData(-2.70, 5.00, -0.53, -4.03, 3.4, 0.39, false)]
    [InlineData(-4.77, -3.58, 4.52, -4.49, -1.7, 1.70, false)]
    [InlineData(2.48, -3.28, 0.42, -1.00, 1.2, 2.50, false)]
    [InlineData(-1.16, 0.79, -3.16, 2.62, 1.2, -1.43, true)]
    [InlineData(-2.30, 3.63, 3.13, -4.58, 1.9, -2.71, true)]
    [InlineData(3.41, 3.31, 0.50, -1.25, -2.7, -3.00, false)]
    [InlineData(3.90, 1.50, -1.90, -2.76, -3.0, -3.52, true)]
    [InlineData(2.29, -0.59, -3.02, 4.44, -0.8, 2.42, true)]
    [InlineData(0.98, 4.88, -1.88, 3.11, 2.5, 5.80, true)]
    [InlineData(-3.96, 0.74, 0.42, -1.67, 1.7, -2.30, true)]
    [InlineData(-2.53, 0.86, 5.91, 0.84, 3.0, 0.86, true)]
    [InlineData(2.29, 0.58, -1.25, 3.67, -1.9, 4.22, true)]
    public void ComplexEquationPointTests(
        double x1, double y1, double x2, double y2, double pointX, double pointY, bool isOnLine) {
        Equation2D equation = new Equation2D(new Point2D(x1, y1), new Point2D(x2, y2));

        equation.PointIsOnLine(pointX, pointY, 0.15).Should().Be(isOnLine);
    }
}
public class EquationIntersectionPointTests {
    [Theory]
    [InlineData(1.2, 2.8, 1.2, 5.6, 0, 0, true)]
    [InlineData(1.2, 2.8, 1.2, 2.8, -1.5, 0.93, true)]
    [InlineData(0.7, 5.6, 0.5, -2.9, -42.5, -24.15, false)]
    [InlineData(3.5, 2.5, -4.2, -5.0, -0.9, -0.9, false)]
    [InlineData(-0.2, 5.6, -4.2, 2.5, -0.77, 5.75, false)]
    [InlineData(-4.0, 2.7, 2.0, -0.5, 0.5, 0.56, false)]
    [InlineData(-2.8, 0.0, 3.4, -2.2, 0.3, -0.9, false)]
    [InlineData(-1.0, 5.0, -3.6, 2.2, -1.0, 6.07, false)]
    [InlineData(-2.7, 5.8, -0.7, 5.6, 0.1, 5.5, false)]
    [InlineData(4.1, 4.9, -0.2, 2.5, -0.5, 2.6, false)]
    [InlineData(-1.1, 0.9, -3.1, -2.1, -1.5, 2.5, false)]
    public void IntersectionPointTests(
        double slope1, double intercept1, double slope2, double intercept2, double pointX, double pointY, bool isNull) {
        Equation2D e1 = new Equation2D(slope1, intercept1);
        Equation2D e2 = new Equation2D(slope2, intercept2);
        Point2D? answer = e1.IntersectionPoint(e2);

        if (answer is null) {
            isNull.Should().BeTrue();
        } else {
            isNull.Should().BeFalse();
            answer.X.Should().BeApproximately(pointX, 0.1);
            answer.Y.Should().BeApproximately(pointY, 0.1);
        }

        answer = e1 ^ e2;

        if (answer is null) {
            isNull.Should().BeTrue();
        } else {
            isNull.Should().BeFalse();
            answer.X.Should().BeApproximately(pointX, 0.1);
            answer.Y.Should().BeApproximately(pointY, 0.1);
        }
    }
}
public class EquationOperatorTests {
    [Theory]
    [InlineData(4.42, 8.16)]
    [InlineData(5.37, 8.59)]
    [InlineData(5.79, 10.78)]
    [InlineData(4.38, 2.34)]
    public void InversionTests(
        double slope, double intercept) {
        Equation2D equation = new Equation2D(slope, intercept);
        equation = -equation;
        equation[1].Should().Be(-slope);
        equation[0].Should().Be(intercept);
    }

    [Theory]
    [InlineData(5.79, 3.69, 3.59, 3.54, 9.37, 7.23)]
    [InlineData(6.61, 9.46, 1.73, 9.33, 8.34, 18.79)]
    [InlineData(5.74, 4.10, 9.61, 2.24, 15.35, 6.35)]
    [InlineData(8.92, 4.66, 3.54, 2.62, 12.46, 7.28)]
    [InlineData(10.10, 3.91, 4.55, 2.29, 14.64, 6.20)]
    public void PlusTests(
        double slope1, double intercept1, double slope2, double intercept2, double outSlope, double outIntercept) {
        Equation2D e1 = new Equation2D(slope1, intercept1);
        Equation2D e2 = new Equation2D(slope2, intercept2);
        Equation2D answer = e1 + e2;

        answer[1].Should().BeApproximately(outSlope, 0.1);
        answer[0].Should().BeApproximately(outIntercept, 0.1);
    }

    [Fact]
    public void DoubleEqualsTest() {
        Equation2D e1 = new Equation2D(-1, 1);
        Equation2D e2 = new Equation2D(1, 1);
        Equation2D e3 = new Equation2D(1, 1);

        (e2 == e3).Should().BeTrue();
        (e1 == e2).Should().BeFalse();
        (e1 != e2).Should().BeTrue();
        (e2 != e3).Should().BeFalse();
    }

    [Fact]
    public void GTandLTTest() {
        Equation2D e1 = new Equation2D(-1, 1);
        Equation2D e2 = new Equation2D(1, -1);
        Equation2D e3 = new Equation2D(1, 1);
        Equation2D e4 = new Equation2D(3, 2);

        (e1 < e2).Should().BeTrue();
        (e2 < e3).Should().BeTrue();
        (e3 < e4).Should().BeTrue();
        (e2 < e1).Should().BeFalse();
        (e4 < e1).Should().BeFalse();

        (e1 > e2).Should().BeFalse();
        (e2 > e3).Should().BeFalse();
        (e3 > e4).Should().BeFalse();
        (e2 > e1).Should().BeTrue();
        (e4 > e1).Should().BeTrue();
    }

    [Fact]
    public void GTETandLTETTest() {
        Equation2D e1 = new Equation2D(-1, 1);
        Equation2D e2 = new Equation2D(1, -1);
        Equation2D e3 = new Equation2D(1, 1);
        Equation2D e4 = new Equation2D(3, 2);

        (e1 <= e2).Should().BeTrue();
        (e2 <= e3).Should().BeTrue();
        (e3 <= e4).Should().BeTrue();
        (e2 <= e1).Should().BeFalse();
        (e4 <= e1).Should().BeFalse();

        (e1 >= e2).Should().BeFalse();
        (e2 >= e3).Should().BeTrue();
        (e3 >= e4).Should().BeFalse();
        (e2 >= e1).Should().BeTrue();
        (e4 >= e1).Should().BeTrue();
    }
}
public class EquationInterfaceImplementationTests {
    [Theory]
    [InlineData(-1, 1, 2, 0, false)]
    [InlineData(-2, -1, 1, 0, false)]
    [InlineData(2, 1, 2, 1, true)]
    [InlineData(-2, -2, -1, -2, false)]
    [InlineData(1, -1, 2, 1, false)]
    public void EqualityTests(
        double slope1, double intercept1, double slope2, double intercept2, bool isEqual) {
        Equation2D e1 = new Equation2D(slope1, intercept1);
        Equation2D e2 = new Equation2D(slope2, intercept2);

        e1.Equals(e2).Should().Be(isEqual);
    }

    [Theory]
    [InlineData(1, -2, 1, -2, 0)]
    [InlineData(0, 2, -2, 1, 1)]
    [InlineData(0, -1, 1, 0, -1)]
    [InlineData(-2, -1, 1, -2, -1)]
    [InlineData(-2, -1, 0, 1, -1)]
    [InlineData(2, 2, 0, -1, 1)]
    [InlineData(-2, 2, -1, -2, -1)]
    [InlineData(1, 2, 0, -1, 1)]
    [InlineData(1, 0, 1, 1, -1)]
    [InlineData(2, 1, -1, 1, 1)]
    [InlineData(2, 1, 0, 1, 1)]
    [InlineData(-1, 2, 1, -1, -1)]
    [InlineData(-1, 1, 1, 2, -1)]
    [InlineData(-2, -2, -1, 2, -1)]
    [InlineData(0, -2, -2, 1, 1)]
    [InlineData(0, 2, -1, 1, 1)]
    [InlineData(-2, -2, 1, 0, -1)]
    [InlineData(0, 0, -2, 1, 1)]
    [InlineData(-2, -2, 0, 1, -1)]
    public void CompareToTests(
        double slope1, double intercept1, double slope2, double intercept2, int outValue) {
        Equation2D e1 = new Equation2D(slope1, intercept1);
        Equation2D e2 = new Equation2D(slope2, intercept2);

        e1.CompareTo(e2).Should().Be(outValue);
    }
}
public class VerticalEquationTests {
    [Fact]
    public void CanInitialize() {
        Equation2D equation = new Equation2D(5, 5, 5, 0);
    }

    [Fact]
    public void InitializeIsCorrect() {
        Equation2D equation = new Equation2D(5, 5, 5, 0);
        equation.Intercept.Should().Be(5);
    }

    [Theory] // Validated with Desmos
    [InlineData(0.00, 4.59, 5, 4.59)]
    [InlineData(-0.25, -3.81, 5, -5.06)]
    [InlineData(1.92, 0.01, 5, 9.61)]
    [InlineData(-3.37, -0.16, 5, -17.01)]
    [InlineData(-3.61, -4.97, 5, -23.02)]
    [InlineData(3.71, -4.17, 5, 14.38)]
    [InlineData(-4.05, 2.41, 5, -17.84)]
    [InlineData(4.16, 4.36, 5, 25.16)]
    [InlineData(5.31, 2.83, 5, 29.38)]
    [InlineData(5.66, -0.57, 5, 27.73)]
    public void VerticalLineIntersectionPoints(
        double slope, double intercept, double intersectionX, double intersectionY) {
        Equation2D e1 = new Equation2D(slope, intercept);
        Equation2D e2 = new Equation2D(5, 5, 5, 0);

        Point2D? answer = e1.IntersectionPoint(e2);
        if (answer is not null) {
            answer.X.Should().BeApproximately(intersectionX, 0.1);
            answer.Y.Should().BeApproximately(intersectionY, 0.1);
        }
    }

    [Fact]
    public void VerticalLineNullTest() {
        Equation2D e1 = new Equation2D(3, 3, 3, 0);
        Equation2D e2 = new Equation2D(5, 5, 5, 0);

        Point2D? answer = e1.IntersectionPoint(e2);
        answer.Should().BeNull();
    }

    [Theory]
    [InlineData(5.61, 5.04, false)]
    [InlineData(0.89, 4.50, false)]
    [InlineData(3.005, 2.95, true)]
    [InlineData(5.80, 1.58, false)]
    [InlineData(3.70, 3.07, false)]
    [InlineData(2.3, 5.94, false)]
    public void PointIsOnLineTests(
        double x, double y, bool isOnLine) {
        Equation2D e1 = new Equation2D(3, 3, 3, 0);

        e1.PointIsOnLine(x, y).Should().Be(isOnLine);
    }
}

