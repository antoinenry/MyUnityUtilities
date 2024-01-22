# Segment Utilities
- A segment is basically a pair of comparable values
- Includes an interface with handy operations between segments: length, containment, intersection, junction...
- Int and float segment are implemented but the tool can (and will) easily be expended to Vectors
- Simple property drawer

# Single Scriptable Object Utilities
- A very small addition to its base class ScriptableObject
- Adds a "Current" static reference and a "isCurrent" bool to instances
- Toggle one instance as "isCurrent" automatically updates the static  "Current" and the other instances' "isCurrent"
- Facilitates singleton-ish design: the class (abstract) allows multiple instances but ensures there's only one "Current"
