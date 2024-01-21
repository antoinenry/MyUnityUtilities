using System;

static public class SegmentOperations<S, T>
    where S : struct, ISegment<T> 
    where T : struct, IComparable
{
    static public string ToString(ISegment<T> s)
    {
        return "[" + s.A + "; " + s.B + "]";
    }

    static public ISegment<T> Invert(ISegment<T> s)
    {
        ISegment<T> invert = s.Clone();
        invert.A = s.B;
        invert.B = s.A;
        return invert;
    }

    static public int Direction(ISegment<T> s)
    {
        return s.B.CompareTo(s.A);
    }

    static public bool Contains(ISegment<T> s, T t, bool strict = false) 
    {
        if (strict)
        {
            return t.CompareTo(s.A) == -t.CompareTo(s.B)
                && t.CompareTo(s.A) != 0
                && t.CompareTo(s.B) != 0;
        }
        else
        {
            return t.CompareTo(s.A) == -t.CompareTo(s.B)
                || t.CompareTo(s.A) == 0 
                || t.CompareTo(s.B) == 0;
        }
    }

    static public bool Contains(ISegment<T> s1, ISegment<T> s2, bool strict = false)
    {
        return Contains(s1, s2.A, strict) && Contains(s1, s2.B, strict);
    }

    static public bool Crosses(ISegment<T> s1, ISegment<T> s2, bool strict = false)
    {
        return Contains(s1, s2.A, strict) || Contains(s1, s2.B, strict) || Contains(s2, s1.A, strict) || Contains(s2, s1.B, strict);
    }

    static public ISegment<T> Intersection(ISegment<T> s1, ISegment<T> s2)
    {
        ISegment<T> intersection = s1.Clone();
        if (Crosses(s1, s2) == false)
        {
            intersection.IsNaN = true;
        }
        else
        {
            if (Direction(s2) != Direction(s1)) s2 = Invert(s2);
            if (Contains(s1, s2.A)) intersection.A = s2.A;
            if (Contains(s1, s2.B)) intersection.B = s2.B;
        }
        return intersection;
    }

    static public ISegment<T>[] Junction(ISegment<T> s1, ISegment<T> s2)
    {
        // Segments are separate: return both
        if (Crosses(s1, s2) == false)
        {
            return new ISegment<T>[2] { s1.Clone(), s2.Clone() };
        }
        // One segment contains the other: return container
        else if (Contains(s1, s2))
        {
            return new ISegment<T>[1] { s1.Clone() };
        }
        else if (Contains(s2, s1))
        {
            return new ISegment<T>[1] { s2.Clone() };
        }
        // Segments are touching: return junction as one segment
        else
        {
            ISegment<T> junction = s1.Clone();
            if (Direction(s1) == Direction(s2))
            {
                if (s1.A.CompareTo(s2.A) == 1) junction.A = s2.A;
                if (s1.B.CompareTo(s2.B) == -1) junction.B = s2.B;
            }
            else
            {
                if (s1.A.CompareTo(s2.B) == 1) junction.A = s2.B;
                if (s1.B.CompareTo(s2.A) == -1) junction.B = s2.A;
            }                
            return new ISegment<T>[1] { junction };
        }
    }

    static public ISegment<T>[] Exclusion(ISegment<T> from, ISegment<T> excluded)
    {
        // If segment is entirely inside the cut part: return no segment
        if (Contains(excluded, from))
        {
            return new ISegment<T>[0];
        }
        // Removed segment direction doesn't matter, make it the same as s1
        if (Direction(excluded) != Direction(from)) excluded = Invert(excluded);
        // Remove something only if there's an intersection
        ISegment<T> intersection = Intersection(from, excluded);
        if (intersection.Length > 0)
        {
            // Cut part is strictly inside segment : returns two segments, "around" intersection
            if (Contains(from, intersection, true))
            {
                ISegment<T>[] around = new ISegment<T>[2] { from.Clone(), from.Clone() };
                around[0].B = excluded.A;
                around[1].A = excluded.B;
                return around;
            }
            // Cut part is partially inside: returns one segment, before or after intersection
            else
            {
                ISegment<T> remain = from.Clone();
                if (from.A.CompareTo(intersection.A) == 0)
                    remain.A = intersection.B;
                else if (from.B.CompareTo(intersection.B) == 0)
                    remain.B = intersection.A;
                else
                {
                    // This shouldn't happen!
                    throw new Exception("Unknown segment error");
                }
                return new ISegment<T>[1] { remain };
            }
        }
        // Nothing to remove
        else
            return new ISegment<T>[1] { from };
    }  
}