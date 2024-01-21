using System;
using UnityEngine;

[System.Serializable]
public struct SegmentFloat : ISegment<float>
{
    [SerializeField] private float a;
    [SerializeField] private float b;

    public SegmentFloat(float a, float b)
    {
        this.a = a;
        this.b = b;
    }

    public SegmentFloat Nan => new SegmentFloat(float.NaN, float.NaN);
    public ISegment<float> Clone() => new SegmentFloat(a, b);

    public override string ToString() => SegmentOperations<SegmentFloat, float>.ToString(this);
    public static implicit operator string(SegmentFloat s) => s.ToString();

    public float A { get => a; set => a = value; }
    public float B { get => b; set => b = value; }
    public bool IsNaN
    {
        get => float.IsNaN(a) || float.IsNaN(b);
        set
        {
            if (value == true)
            {
                a = float.NaN;
                b = float.NaN;
            }
            else
            {
                if (a == float.NaN) a = 0;
                if (b == float.NaN) b = 0;
            }
        }
    }
    public float Length => IsNaN ? -1 : Mathf.Abs(A - B);

    public bool Contains(float t, bool strict = false) => SegmentOperations<SegmentFloat, float>.Contains(this, t, strict);
    public bool Contains(ISegment<float> s, bool strict = false) => SegmentOperations<SegmentFloat, float>.Contains(this, s, strict);
    public bool Crosses(ISegment<float> s, bool strict = false) => SegmentOperations<SegmentFloat, float>.Crosses(this, s, strict);

    public ISegment<float> Invert() => (SegmentFloat)SegmentOperations<SegmentFloat, float>.Invert(this);
    public ISegment<float> Intersect(ISegment<float> s) => SegmentOperations<SegmentFloat, float>.Intersection(this, s);
    public ISegment<float>[] Join(ISegment<float> s) => SegmentOperations<SegmentFloat, float>.Junction(this, s);
    public ISegment<float>[] Exclude(ISegment<float> s) => SegmentOperations<SegmentFloat, float>.Exclusion(this, s);
}