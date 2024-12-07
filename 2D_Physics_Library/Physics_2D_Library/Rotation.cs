namespace Physics2DLibrary;

/// <summary>
/// Class <c>Rotation</c> stores a single RotationAngle as an int, and provides a number of helpful 
/// methods and properties to work with 2D angles. You can also interact with it through the Radian 
/// system. It implements the IEquatable and IComparable interfaces.
/// </summary>
public class Rotation : IEquatable<Rotation>, IComparable<Rotation> {
    private int rotationAngle;

    /// <value>
    /// On set, ensures the valid range is between 0 and 359.
    /// </value>
    public int RotationAngle {
        get { return rotationAngle; }
        set {
            int incomingValue = value;
            while (incomingValue < 0) {
                incomingValue += 360;
            }
            rotationAngle = incomingValue % 360;
        }
    }

    /// <value>
    /// On set, it parses to an angle and sends it through the other property setter.
    /// </value>
    public double RotationRadian {
        get {
            return Math.Round(rotationAngle * Math.PI / 180, 3);
        }
        set {
            RotationAngle = (int)Math.Round(value / Math.PI * 180);
        }
    }

    /// <value>
    /// Gets the X coordinate of the angle on a unit circle (as a radius 
    /// of 1).
    /// </value>
    public double X_Coord {
        get {
            double radians = (Math.PI / 180) * rotationAngle;
            return Math.Round(Math.Cos(radians), 2);
        }
    }

    /// <value>
    /// Gets the Y coordinate of the angle on a unit circle (as a radius 
    /// of 1).
    /// </value>
    public double Y_Coord {
        get {
            double radians = (Math.PI / 180) * rotationAngle;
            return Math.Round(Math.Sin(radians), 2);
        }
    }

    /// <summary>
    /// Default constructor assigns angle to 0.
    /// </summary>
    public Rotation() {
        rotationAngle = 0;
    }

    /// <summary>
    /// Constructor assigns angle to the input angle.
    /// </summary>
    public Rotation(int angle) {
        RotationAngle = angle; // Purposefully calls Property logic
    }

    /// <summary>
    /// Constructor assigns Radian to input value.
    /// </summary>
    public Rotation(double radian) {
        RotationRadian = radian;
    }

    /// <summary>
    /// Constructor takes in a <c>Rotation</c> object and duplicates it.
    /// </summary>
    public Rotation(Rotation value) {
        rotationAngle = value.RotationAngle;
    }

    /// <summary>
    /// Given an input angle, it adds or removes by that value and ensures 
    /// the bounds of 0 - 359.
    /// </summary>
    /// <param name="angleDifference">Positive or negative int to adjust the current angle by</param>
    public void AdjustBy(int angleDifference) {
        RotationAngle += angleDifference;
    }

    /// <summary>
    /// Takes in a pair of coordinates from origin (so convert them to that beforehand or use the other method) 
    /// and assigns the class's rotation to that value. 
    /// </summary>
    public void AssignToCoordinates(double x, double y) {
        RotationRadian = Math.Atan2(y, x);
    }

    /// <summary>
    /// Assigns the class's rotation value to the angle from the first point to the second point.
    /// </summary>
    /// <param name="x1">Current X position</param>
    /// <param name="y1">Current Y position</param>
    /// <param name="x2">Target X position</param>
    /// <param name="y2">Target Y position</param>
    public void AssignToCoordinates(double x1, double y1, double x2, double y2) {
        AssignToCoordinates(x2 - x1, y2 - y1);
    }

    /// <summary>
    /// Takes in a pair of tuples and assigns their coordinates to those points, using an overload
    /// of this method.
    /// </summary>
    public void AssignToCoordinates((double, double) point1, (double, double) point2) {
        AssignToCoordinates(point1.Item1, point1.Item2, point2.Item1, point2.Item2);
    }

    /// <summary>
    /// Flips the angle on the X axis.
    /// </summary>
    public void FlipX() {
        if (rotationAngle < 90) {
            RotationAngle = 360 - rotationAngle;
        } else if (rotationAngle < 180) {
            RotationAngle = 180 + (180 - rotationAngle);
        } else if (rotationAngle < 270) {
            RotationAngle = 180 - (rotationAngle - 180);
        } else {
            RotationAngle = 360 - rotationAngle;
        }
    }

    /// <summary>
    /// Flips the angle on the Y axis.
    /// </summary>
    public void FlipY() {
        if (rotationAngle < 90) {
            RotationAngle = 90 + (90 - rotationAngle);
        } else if (rotationAngle < 180) {
            RotationAngle = 180 - rotationAngle;
        } else if (rotationAngle < 270) {
            RotationAngle = 270 + (270 - rotationAngle);
        } else {
            RotationAngle = 270 - (rotationAngle - 270);
        }
    }

    /// <summary>
    /// Flips the angle on both axes.
    /// </summary>
    public void DoubleFlip() {
        RotationAngle -= 180;
    }

    /// <summary>
    /// Returns the closest distance ( &lt;= 180 ) to the given object.
    /// </summary>
    public int DistanceTo(Rotation angle) {
        return DistanceTo(angle.RotationAngle);
    }

    /// <summary>
    /// Returns the closest distance ( &lt;= 180 ) to the given angle.
    /// </summary>
    public int DistanceTo(int angle) {
        int localMin = Math.Min(
            Math.Abs((angle + 360) - rotationAngle),
            Math.Abs(angle - (rotationAngle + 360)));
        return Math.Min(
            Math.Abs(angle - rotationAngle),
            localMin);
    }

    /// <summary>
    /// Averages between this and the "other" parameter by a percentage. 0 maintains the current angle, 
    /// and 1 sets it to the other angle. This just passes through to the other overload.
    /// </summary>
    /// <param name="other">Rotation to average to</param>
    /// <param name="percent">Range from 0 to 1</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void AverageTo(Rotation other, double percent) {
        AverageTo(other.RotationAngle, percent);
    }

    /// <summary>
    /// Averages between this and the "other" parameter by a percentage. 0 maintains the current angle, 
    /// and 1 sets it to the other angle. 
    /// </summary>
    /// <param name="angle">Angle to average to</param>
    /// <param name="percent">Range from 0 to 1</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void AverageTo(int angle, double percent) {
        if (percent < 0 || percent > 1) { throw new ArgumentOutOfRangeException("Percent must be between 0 and 1."); }

        int diff = angle - rotationAngle;
        int distance = DistanceTo(angle);

        if (diff > 180 || diff < 0) { distance *= -1; }

        distance = (int) Math.Round(distance * percent);
        AdjustBy(distance);
    }

    /// <summary>
    /// IEquatable interface implementation for equality.
    /// </summary>
    public bool Equals(Rotation? other) {
        return rotationAngle == other?.rotationAngle;
    }

    /// <summary>
    /// IComparable interface implementation.
    /// </summary>
    /// <returns>&lt; 0 for a smaller object, 0 for the same position, and &gt; 0 for a larger object</returns>
    public int CompareTo(Rotation? other) {
        if (other is null) return 1;
        return RotationAngle.CompareTo(other.RotationAngle);
    }

    /// <summary>
    /// Adds one rotation angle to the other, and returns a normalized angle.
    /// </summary>
    /// <returns>New <c>Rotation</c> object</returns>
    public static Rotation operator +(Rotation left, Rotation right) {
        return new Rotation(left.RotationAngle + right.RotationAngle);
    }

    /// <summary>
    /// Subtracts one rotation angle from the other, and returns a normalized angle.
    /// </summary>
    /// <returns>New <c>Rotation</c> object</returns>
    public static Rotation operator -(Rotation left, Rotation right) {
        return new Rotation(left.RotationAngle - right.RotationAngle);
    }

    /// <summary>
    /// Averages the two angles together (with a separation above 180, so 0 and 180 returns 90). 
    /// </summary>
    /// <returns>New <c>Rotation</c> with the average angle set</returns>
    public static Rotation operator ^(Rotation left, Rotation right) {
        double diff = Math.Abs(left.RotationAngle - right.RotationAngle);
        int average = (left.RotationAngle + right.RotationAngle) / 2;

        if (diff > 180) { average += 180; }

        return new Rotation(average);
    }

    /// <summary>
    /// Returns a new rotation with the first modulo'd by the second angle (angle, not radians)
    /// </summary>
    /// <param name="left">Rotation to be modulo'd</param>
    /// <param name="right">Rotation to do the modulo-ing</param>
    /// <returns>New <c>Rotation</c> object</returns>
    public static Rotation operator %(Rotation left, Rotation right) {
        return new Rotation(left.RotationAngle % right.RotationAngle);
    }
}
