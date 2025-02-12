# 2D Physics Library

This is a junior-in-school's first public library. As of now, it contains the Rotation class, a well-tested and 
defined method for working with coordinates on a 2D plane. It gets and sets Radians and Angles (with accuracy of 1 degree), 
provides X and Y coordinates of the unit circle, and can be assigned to point at some arbitrary point in space. A few more 
methods will be defined for this class.

In the future, this package will have a number of additional classes that right now are focused on top-down 2D programs 
(that don't generally have to deal with a downward force such as gravity). It will take care of raycasting, object 
line definitions, overlaps, and maybe include some math classes to calculate more complex physics interactions. 

I add XML Comments (the ones VS Code and Visual Studio usually read for their Intellisense) to every property, method, and class, 
so I hope those are of a bit more use to you than a generalized readme. I also intend to add an external documentation page 
for all the classes and methods I generate here. That is for some future day, though. 

## Changelog

0.?.? (?/?/?)
	
	- SOME BREAKING CHANGES: Adjusted class names to more closely align with the library name, and the Equation name conflict. 
		- There's a default class with the name Equation, so I had to rename everything.. welp!
		- Rotation2D no longer supports Radians. A second RotationRadian2D class will bea added to work with radians instead, if necessary. I also added 2 decimals of precision to my angles.  
	- Added more operators to Rotation class
	- Added all overrides to Rotation class (GetHashCode, Equals, and ToString)
	- Created Point class
		- Has methods for Midpoint, Scaling, Angles from each other, Distance, and a handful of helpful operators
	- Created Equation(2D) class
		- Defines a line
		- Can determine points along that line
		- Can determine an intersection point
	- Created VectorObject
	- Standardized a lot of my methodology for randomized testing and ensuring I hit every method at least once. 
	- Testing is not complete; library has about ~~10~~ 20 hours more of finishing touches. 

0.2.5 (12/7/24) 

	- Completed Rotation class
	- DistanceTo, AverageTo, Cloning Constructor, IEquatable, and IComparable methods added
	- Overrode operators + (addition), - (subtraction), % (modulo), and ^ (average of two angles)
	- Ensured XML comments were included in the package
	- Test cases up to 148

0.2.0 (12/6/24) 

	- Added Rotation class