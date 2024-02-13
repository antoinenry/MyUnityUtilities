# Current Asset Manager
- Basically my personal workaround for something that behaves like a "Singleton ScriptableObject"
- Manages a list of Unity Objects that user would want to mark as "Current" (no more than one object of the same type)
- Allows to access those object in a static fashion 
- Objects can be added manually to the list
- Also includes a PropertyAttribute which, when coupled to a bool in a ScriptableObject (bool isCurrent) allows to set the "current" directly from the Object's inspector

Notes:
- ScriptableSingleton<T> exists but only for Editor purposes (excluded from build)

# Segment Utilities
- A segment is basically a pair of comparable values
- Includes an interface with handy operations between segments: length, containment, intersection, junction...
- Int and float segment are implemented but the tool can (and will) easily be expended to Vectors
