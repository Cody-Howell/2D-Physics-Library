namespace Physics2DLibrary;

/// <summary>
/// Class <c>Rotation</c> stores a single RotationAngle as an int, and provides a number of helpful 
/// methods and properties to work with 2D angles. You can also interact with it through the Radian 
/// system.
/// </summary>
public class Rotation {
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
        } set {
            RotationAngle = (int) Math.Round(value / Math.PI * 180);
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

    // Add second AssignCoords method here overriden with two different points that finds the angle from one to another

    /// <summary>
    /// Flips the angle on the X axis.
    /// </summary>
    public void FlipX() {
        if (rotationAngle < 90) {
            RotationAngle = 360 - rotationAngle;
        }
        else if (rotationAngle < 180) {
            RotationAngle = 180 + (180 - rotationAngle);
        }
        else if (rotationAngle < 270) {
            RotationAngle = 180 - (rotationAngle - 180);
        }
        else {
            RotationAngle = 360 - rotationAngle;
        }
    }

    /// <summary>
    /// Flips the angle on the Y axis.
    /// </summary>
    public void FlipY() {
        if (rotationAngle < 90) {
            RotationAngle = 90 + (90 - rotationAngle);
        }
        else if (rotationAngle < 180) {
            RotationAngle = 180 - rotationAngle;
        }
        else if (rotationAngle < 270) {
            RotationAngle = 270 + (270 - rotationAngle);
        }
        else {
            RotationAngle = 270 - (rotationAngle - 270);
        }
    }

    // Override adding (+) for averaging the two rotations
    // Add a method for "averaging" within a certain percentage of the second one
        // Would go like AverageTo(Rotation r, double percent) and return moving that percentage to the new value
}
