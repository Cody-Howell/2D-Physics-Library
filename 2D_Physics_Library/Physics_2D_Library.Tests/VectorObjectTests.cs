using FluentAssertions;
namespace Physics2DLibrary.Tests.VectorObject;

public class VectorObjectSimpleConstructorTests {
    [Fact]
    public void Ctor1() {
        VectorObject2D obj = new VectorObject2D();
        obj.CenterPoint.Equals(new Point2D()).Should().BeTrue();
    }

    [Fact]
    public void Ctor2() {
        VectorObject2D obj = new VectorObject2D(new Point2D(3, 4), new List<Vector2D> { new Vector2D(0, 1), new Vector2D(90, 1) });
        obj.CenterPoint.Equals(new Point2D(3, 4)).Should().BeTrue();
        obj[0].Equals(new Point2D(4, 4)).Should().BeTrue();
        obj[1].Equals(new Point2D(3, 5)).Should().BeTrue();
    }

    [Fact]
    public void Ctor3() {
        VectorObject2D obj = new VectorObject2D(new Point2D(3, 4), new List<Point2D> { new Point2D(4, 4), new Point2D(3, 5) });
        obj.CenterPoint.Equals(new Point2D(3, 4)).Should().BeTrue();
        obj[0].Equals(new Point2D(4, 4)).Should().BeTrue();
        obj[1].Equals(new Point2D(3, 5)).Should().BeTrue();
    }
}
public class VectorObjectEnumerationTests {
    [Fact]
    public void DefaultEnumerator() {
        VectorObject2D obj = new VectorObject2D(new Point2D(3, 4), new List<Point2D> { new Point2D(4, 4), new Point2D(3, 5) });
        Assert.Throws<EntryPointNotFoundException>(obj.GetEnumerator);

        //foreach (Point2D point in obj) {
        //    // Should throw error, just for display.
        //}
    }

    [Fact]
    public void PointEnumerator() {
        VectorObject2D obj = new VectorObject2D(new Point2D(3, 4), new List<Point2D> { new Point2D(4, 4), new Point2D(3, 5) });
        Point2D[] answers = { new Point2D(4, 4), new Point2D(3, 5) };
        int answersIndex = 0;
        foreach (Point2D point in obj.GetPoints()) {
            answers[answersIndex++].Equals(point).Should().BeTrue();
        }
    }

    [Fact]
    public void LineEnumerator() {
        VectorObject2D obj = new VectorObject2D(new Point2D(0, 0), new List<Point2D> { 
            new Point2D(1, 0), new Point2D(0, 1),
            new Point2D(-1, 0), new Point2D(0, -1)
        });
        Line2D[] answers = { 
            new Line2D(1, 0, 0, 1),
            new Line2D(0, 1, -1, 0),
            new Line2D(-1, 0, 0, -1),
            new Line2D(0, -1, -1, 0)
        };
        int answersIndex = 0;
        foreach (Line2D line in obj.GetLines()) {
            answers[answersIndex++].Equals(line).Should().BeTrue();
        }
    }
}
public class VectorObjectPointWithinTests {
    [Theory]
    [InlineData(4, 9, true)]
    [InlineData(-10, 15, false)]
    [InlineData(6, 0.1, true)]
    [InlineData(20, 2, false)]
    [InlineData(9, 19, false)]
    [InlineData(8, 2, true)]
    [InlineData(2, 17, false)]
    [InlineData(4, -3, false)]
    [InlineData(12, -5, false)]
    [InlineData(-3, 19, false)]
    [InlineData(9, 6, true)]
    [InlineData(-1, 13, false)]
    [InlineData(5, 5, true)]
    public void ActualInsideTests(
    double x, double y, bool isInside) {
        List<Point2D> points = new List<Point2D> {
            new Point2D(0, 10),
            new Point2D(10, 10),
            new Point2D(10, 0),
            new Point2D(0, 0)
        };
        VectorObject2D obj = new VectorObject2D(new Point2D(5, 5), points);
        obj.IsPointWithin(new Point2D(x, y)).Should().Be(isInside);
    }
}
public class VectorObjectEqualityTests {
    [Fact]
    public void BasicTest1() {
        VectorObject2D v1 = new VectorObject2D(new Point2D(0, 0), new List<Point2D> { new Point2D(1, 2), new Point2D(1, 2), new Point2D(1, 2) });
        VectorObject2D v2 = new VectorObject2D(new Point2D(0, 0), new List<Point2D> { new Point2D(1, 2), new Point2D(1, 2), new Point2D(1, 2) });

        v1.Equals(v2).Should().BeTrue();
    }
}
public class VectorObjectRotateByTests {
    [Fact]
    public void EmptyTest() {
        VectorObject2D obj = new VectorObject2D(
            new Point2D(0, 0), 
            new List<Vector2D> {
                new Vector2D(0, 1),
                new Vector2D(90, 1),
                new Vector2D(180, 1),
                new Vector2D(270, 1)
            });
        obj.RotateBy(new List<IPointObject2D>(), 90);

        List<Point2D> newPoints = new List<Point2D>(obj);
        newPoints[0].Equals(new Point2D(0, 1)).Should().BeTrue();
        newPoints[1].Equals(new Point2D(-1, 0)).Should().BeTrue();
        newPoints[2].Equals(new Point2D(0, -1)).Should().BeTrue();
        newPoints[3].Equals(new Point2D(1, 0)).Should().BeTrue();
    }

    //[Fact]
    //public void NextToWallTest() {
    //    // (1, 0), (0, 1), (-1, 0), (0, -1)
    //    VectorObject2D obj = new VectorObject2D(
    //        new Point2D(0, 0),
    //        new List<Vector2D> {
    //            new Vector2D(0, 1),
    //            new Vector2D(90, 1),
    //            new Vector2D(180, 1),
    //            new Vector2D(270, 1)
    //        });

    //    // (-1, 2), (0, 3), (3, 0), (2, -1)
    //    VectorObject2D wall1 = new VectorObject2D(
    //        new Point2D(1, 1),
    //        new List<Point2D> {
    //                    new Point2D(-1, 2),
    //                    new Point2D(0, 3),
    //                    new Point2D(3, 0),
    //                    new Point2D(2, -1)
    //        });
    //    VectorObject2D wall2 = new VectorObject2D( // Validating exclusion filtering
    //        new Point2D(100, 100),
    //            new List<Point2D> {
    //                    new Point2D(80, 80),
    //                    new Point2D(80, 120),
    //                    new Point2D(120, 120),
    //                    new Point2D(120, 80)
    //        });

    //    // (1, 0), (0, 1), (-1, 0), (0, -1)
    //    VectorObject2D answer = new VectorObject2D(
    //        new Point2D(0, 0),
    //        new List<Vector2D> {
    //            new Vector2D(0, 1),
    //            new Vector2D(90, 1),
    //            new Vector2D(180, 1),
    //            new Vector2D(270, 1)
    //        });

    //    obj.RotateBy(new List<IPointObject2D>() { wall1, wall2 }, 30);

    //    obj.Equals(answer).Should().BeTrue();
    //}
}
