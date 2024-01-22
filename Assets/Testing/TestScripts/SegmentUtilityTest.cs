using UnityEngine;

[ExecuteAlways]
public class SegmentUtilityTest : MonoBehaviour
{
    [Header("Test Segments (int)")]
    public SegmentInt segment1;
    public SegmentInt segment2;
    [TextArea]
    public string ouput12;
    [Header("Test Segments (float)")]
    public SegmentFloat segment3;
    public SegmentFloat segment4;
    [TextArea]
    public string output34;

    private void Update()
    {
        ouput12 = TestOutput(segment1, segment2);
        output34 = TestOutput(segment3, segment4);
    }

    private string TestOutput<T>(ISegment<T> s1, ISegment<T> s2) where T : struct
    {
        return
            "Length(1) : " + s1.Length + "\r\n" +
            "Length(2) : " + s2.Length + "\r\n" +
            "1.Contains(2) : " + s1.Contains(s2) + "\r\n" +
            "2.Contains(1) : " + s2.Contains(s1) + "\r\n" +
            "1.Crosses(2) : " + s1.Crosses(s2) + "\r\n" +
            "Invert(1) : " + s1.Invert() + "\r\n" +
            "Intersection(1,2) : " + s1.Intersect(s2) + "\r\n" +
            "Junction(1,2) : " + SegmentArrayToString(s1.Join(s2)) + "\r\n" +
            "Exclusion(1,2) : " + SegmentArrayToString(s1.Exclude(s2)) + "\r\n";
    }

    private string SegmentArrayToString<T>(ISegment<T>[] segments) where T : struct
    {
        if (segments == null) return "NULL";
        if (segments.Length == 0) return "(none)";
        string output = "";
        foreach (ISegment<T> s in segments) output += s + ", ";
        return output;
    }
}
